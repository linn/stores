namespace Linn.Stores.Domain.LinnApps.Tpk
{
    using System;
    using System.Linq;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
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
            IQueryRepository<SalesOrder> salesOrderRepository)
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
        }

        public TpkResult TransferStock(TpkRequest tpkRequest)
        {
            var candidates = tpkRequest.StockToTransfer.ToList();
            var from = candidates.First();
            if (candidates.Any(s => s.FromLocation != from.FromLocation))
            {
                throw new TpkException("You can only TPK one pallet at a time");
            }
            
            var recordsToTpk = this.tpkView.FilterBy(r => r.FromLocation == from.FromLocation).Count();
            
            if (recordsToTpk != candidates.Count)
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
            
            var whatToWand = this.whatToWandService.WhatToWand(from.LocationId, from.PalletNumber).ToList();

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
            return new TpkResult 
                       {
                           Success = true,
                           Message = "TPK Successful",
                           Transferred = transferredWithNotes,
                           Report = new WhatToWandReport
                                        {
                                            Account = this.salesAccountQueryRepository
                                                .FindBy(o => o.AccountId == consignment.SalesAccountId),
                                            Consignment = consignment,
                                            Type = this.tpkPack.GetWhatToWandType(consignment.ConsignmentId),
                                            Lines = whatToWand,
                                            CurrencyCode = this.salesOrderRepository
                                                .FindBy(o => o.OrderNumber == whatToWand.First().OrderNumber)
                                                .CurrencyCode, 
                                            TotalNettValueOfConsignment = whatToWand.Sum(x => this.salesOrderDetailRepository
                                                .FindBy(d => d.OrderNumber == x.OrderNumber && d.OrderLine == x.OrderLine).NettTotal),
                                        },
                                     };
        }
    }
}
