namespace Linn.Stores.Domain.LinnApps.Tpk
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;

    using Linn.Common.Logging;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Domain.LinnApps.Requisitions;
    using Linn.Stores.Domain.LinnApps.Tpk.Models;

    public class TpkService : ITpkService
    {
        private readonly IQueryRepository<TransferableStock> tpkView;

        private readonly IQueryRepository<AccountingCompany> accountingCompaniesRepository;

        private readonly ITpkPack tpkPack;

        private readonly IBundleLabelPack bundleLabelPack;

        private readonly IWhatToWandService whatToWandService;

        private readonly IStoresPack storesPack;

        private readonly IQueryRepository<SalesAccount> salesAccountQueryRepository;

        private readonly IRepository<Consignment, int> consignmentRepository;

        private readonly IQueryRepository<SalesOrderDetail> salesOrderDetailRepository;

        private readonly IQueryRepository<SalesOrder> salesOrderRepository;

        private readonly IRepository<ReqMove, ReqMoveKey> reqMovesRepository;

        private readonly IQueryRepository<ProductUpgradeRule> productUpgradeRulesRepository;

        private readonly ILog logger;

        private readonly IProductUpgradePack productUpgradePack;

        public TpkService(
            IQueryRepository<TransferableStock> tpkView,
            IQueryRepository<AccountingCompany> accountingCompaniesRepository,
            ITpkPack tpkPack,
            IBundleLabelPack bundleLabelPack,
            IWhatToWandService whatToWandService,
            IQueryRepository<SalesAccount> salesAccountQueryRepository,
            IStoresPack storesPack,
            IRepository<Consignment, int> consignmentRepository,
            IQueryRepository<SalesOrderDetail> salesOrderDetailRepository,
            IQueryRepository<SalesOrder> salesOrderRepository,
            IRepository<ReqMove, ReqMoveKey> reqMovesRepository,
            IQueryRepository<ProductUpgradeRule> productUpgradeRulesRepository,
            ILog logger,
            IProductUpgradePack productUpgradePack)
        {
            this.tpkView = tpkView;
            this.tpkPack = tpkPack;
            this.bundleLabelPack = bundleLabelPack;
            this.whatToWandService = whatToWandService;
            this.storesPack = storesPack;
            this.salesAccountQueryRepository = salesAccountQueryRepository;
            this.accountingCompaniesRepository = accountingCompaniesRepository;
            this.consignmentRepository = consignmentRepository;
            this.salesOrderDetailRepository = salesOrderDetailRepository;
            this.salesOrderRepository = salesOrderRepository;
            this.reqMovesRepository = reqMovesRepository;
            this.productUpgradeRulesRepository = productUpgradeRulesRepository;
            this.logger = logger;
            this.productUpgradePack = productUpgradePack;
        }

        public TpkResult TransferStock(TpkRequest tpkRequest)
        {
            var candidates = tpkRequest.StockToTransfer.ToList();

            var from = candidates.First();

            var message =
                $"LocationId: {from.LocationId}. " 
                + $"PalletNumber: {from.PalletNumber}. " 
                + $"Consignment: {from.ConsignmentId}.";

            IEnumerable<WhatToWandLine> whatToWand = new List<WhatToWandLine>();
            if (candidates.Any(s => s.FromLocation != from.FromLocation))
            {
                throw new TpkException("You can only TPK one pallet at a time");
            }
            
            var recordsToTpk = this.tpkView.FilterBy(r => r.FromLocation == from.FromLocation);
            
            if (recordsToTpk.Count() != candidates.Count)
            {
                throw new TpkException("You haven't looked at everything from location " + from.FromLocation);
            }
            
            var latestAllocationDateTime = this.accountingCompaniesRepository.FindBy(c => c.Name == "LINN")
                .LatesSalesAllocationDate;
            
            if (tpkRequest.DateTimeTpkViewQueried < latestAllocationDateTime)
            {
                throw new TpkException("Another allocation was run at " + latestAllocationDateTime +
                ". You must re-query the TPK screen to get an up to date version.");
            }
            
            var transferredWithNotes = candidates.Select(
                s => new TransferredStock(s, this.tpkPack.GetTpkNotes(s.ConsignmentId, s.FromLocation)));
            
            this.bundleLabelPack.PrintTpkBoxLabels(from.FromLocation);

            try
            {
                if (this.whatToWandService.ShouldPrintWhatToWand(from.FromLocation))
                {
                    whatToWand = this.whatToWandService.WhatToWand(
                        from.LocationId, from.PalletNumber).ToList();
                }
            }
            catch (Exception ex)
            {
                this.logger.Write(
                    LoggingLevel.Error,
                    Enumerable.Empty<LoggingProperty>(), 
                    $"{message}. {ex.Message}",
                    ex);
                throw new
                    TpkException($"An  error occurred  - phone IT support. {message}.  {ex.Message}");
            }

            this.tpkPack.UpdateQuantityPrinted(from.FromLocation, out var updateQuantitySuccessful);
            
            if (!updateQuantitySuccessful)
            {
                throw new TpkException("Failed in update_qty_printed.");
            }
            
            this.storesPack.DoTpk(from.LocationId, from.PalletNumber, DateTime.Now, out var tpkSuccessful);
            if (!tpkSuccessful)
            {
                throw new TpkException(this.storesPack.GetErrorMessage());
            }

            try
            {
                foreach (var line in whatToWand)
                {
                    var hasUpgradeRule = this.productUpgradeRulesRepository
                        .FilterBy(r  => r.ArticleNumber == line.ArticleNumber && !string.IsNullOrEmpty(r.RenewProduct)).Any();

                    if (hasUpgradeRule)
                    {
                        if (int.TryParse(line.OldSernos, out var oldSernos))
                        {
                            var renewSernos = this.productUpgradePack.GetRenewSernosFromOriginal(oldSernos);
                            line.SerialNumberComments +=
                                $" *** Please select one of the following renew serial numbers: {renewSernos} (Original serial number: {line.OldSernos}) ***";
                        }
                    }
                }

                var consignmentGroups = whatToWand?.GroupBy(x => x.ConsignmentId).ToList();
                return new TpkResult
                           {
                               Success = true,
                               Message = "TPK Successful",
                               Transferred = transferredWithNotes,
                               Consignments = consignmentGroups?.Select(
                                   x =>
                                       {
                                           var consignment = this.consignmentRepository.FindById(x.Key);
                                           return new WhatToWandConsignment
                                                      {
                                                          Lines = x.ToList(), 
                                                          Consignment = consignment, 
                                                          Type = this.tpkPack.GetWhatToWandType(consignment.ConsignmentId, from.FromLocation), 
                                                          Account = this.salesAccountQueryRepository.FindBy(
                                                                                        o => o.AccountId == consignment.SalesAccountId),
                                                          TotalNettValueOfConsignment = x.Sum(l => this.salesOrderDetailRepository.FindBy(
                                                              d => d.OrderNumber == l.OrderNumber
                                                                   && d.OrderLine == l.OrderLine).NettTotal),
                                                          CurrencyCode = this.salesOrderRepository.FindBy(
                                                                  o => o.OrderNumber == x.First().OrderNumber)
                                                              .CurrencyCode
                                           };
                                       })
                           };
            }
            catch (Exception ex)
            {
                return new TpkResult
                           {
                               Success = false, 
                               Message = $"Error generating report. Stock transfer likely still succeeded. Click refresh to check. Error details: {ex.Message}"
                           };
            }
        }

        public ProcessResult UnpickStock(
            int reqNumber, 
            int lineNumber, 
            int orderNumber, 
            int orderLine, 
            int amendedBy,
            int? palletNumber,
            int? locationId)
        {
            var moves = this.reqMovesRepository.FilterBy(
                x => x.ReqNumber == reqNumber 
                     && x.LineNumber == lineNumber 
                     && !x.DateCancelled.HasValue
                     && (x.StockLocator.PalletNumber == palletNumber || x.StockLocator.LocationId == locationId));

            foreach (var reqMove in moves)
            {
                var result = this.storesPack.UnpickStock(
                    reqNumber,
                    lineNumber,
                    reqMove.Sequence,
                    orderNumber,
                    orderLine,
                    reqMove.Quantity,
                    reqMove.StockLocatorId,
                    amendedBy);

                if (!result.Success)
                {
                    return result;
                }
            }

            foreach (var reqMove in moves)
            {
                reqMove.DateCancelled = DateTime.Now;
            }

            return new ProcessResult(true, string.Empty);
        }

        public WhatToWandConsignment ReprintWhatToWand(int consignmentId)
        {
            var consignment = this.consignmentRepository.FindById(consignmentId);

            if (consignment == null)
            {
                throw new NotFoundException("Consignment Not Found");
            }

            var account = this.salesAccountQueryRepository.FindBy(o => o.AccountId == consignment.SalesAccountId);
            var data = this.whatToWandService.ReprintWhatToWand(consignmentId, consignment.Address.CountryCode).ToList();
            
            foreach (var line in data)
            {
                var hasUpgradeRule = this.productUpgradeRulesRepository
                    .FilterBy(r => r.ArticleNumber == line.ArticleNumber && !string.IsNullOrEmpty(r.RenewProduct)).Any();

                if (hasUpgradeRule)
                {
                    if (int.TryParse(line.OldSernos, out var oldSernos))
                    {
                        var renewSernos = this.productUpgradePack.GetRenewSernosFromOriginal(oldSernos);
                        line.SerialNumberComments +=
                            $" *** Please select one of the following renew serial numbers: {renewSernos} (Original serial number: {line.OldSernos}) ***";
                    }
                }
            }

            var result = new WhatToWandConsignment
                       {
                           Lines = data,
                           Consignment = consignment,
                           Type = "REPRINT",
                           Account = account,
                           TotalNettValueOfConsignment = data.Sum(l => this.salesOrderDetailRepository.FindBy(
                               d => d.OrderNumber == l.OrderNumber
                                    && d.OrderLine == l.OrderLine).NettTotal),
                           CurrencyCode = this.salesOrderRepository.FindBy(
                                   o => o.OrderNumber == data.First().OrderNumber)
                               .CurrencyCode
                       };
            return result;
        }
    }
}
