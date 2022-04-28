namespace Linn.Stores.Domain.LinnApps.Tpk
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Domain.LinnApps.Requisitions;
    using Linn.Stores.Domain.LinnApps.StockLocators;
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
            IRepository<ReqMove, ReqMoveKey> reqMovesRepository)
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
        }

        public TpkResult TransferStock(TpkRequest tpkRequest)
        {
            var candidates = tpkRequest.StockToTransfer.ToList();
            var from = candidates.First();
            IEnumerable<WhatToWandLine> whatToWand = null;
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

            if (this.whatToWandService.ShouldPrintWhatToWand(from.FromLocation))
            {
                whatToWand = this.whatToWandService.WhatToWand(from.LocationId, from.PalletNumber).ToList();
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

            var consignment = this.consignmentRepository.FindById(from.ConsignmentId);
            try
            {
                var whatToWandLines = whatToWand?.ToArray();
                return new TpkResult
                           {
                               Success = true,
                               Message = "TPK Successful",
                               Transferred = transferredWithNotes,
                               Report = whatToWandLines == null ? null : new WhatToWandReport
                                            {
                                                Account =
                                                    this.salesAccountQueryRepository.FindBy(
                                                        o => o.AccountId == consignment.SalesAccountId),
                                                Consignment = consignment,
                                                Type = this.tpkPack.GetWhatToWandType(consignment.ConsignmentId),
                                                Lines = whatToWandLines,
                                                CurrencyCode =
                                                    this.salesOrderRepository.FindBy(
                                                            o => o.OrderNumber == whatToWandLines.First().OrderNumber)
                                                        .CurrencyCode,
                                                TotalNettValueOfConsignment = whatToWandLines.Sum(
                                                    x => this.salesOrderDetailRepository.FindBy(
                                                        d => d.OrderNumber == x.OrderNumber
                                                             && d.OrderLine == x.OrderLine).NettTotal),
                                            }
                           };
            }
            catch (Exception ex)
            {
                return new TpkResult { Success = false, Message = $"Error generating report. Stock transfer likely still succeeded. Click refresh to check. Error details: {ex.Message}" };
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
    }
}
