namespace Linn.Stores.Domain.LinnApps.Tpk
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Persistence;
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

        private readonly IStoresPack storesOoPack;

        private readonly IQueryRepository<SalesOutlet> salesOutletQueryRepository;

        private readonly IQueryRepository<Consignment> consignmentRepository;

        public TpkService(
            IQueryRepository<TransferableStock> tpkView,
            IQueryRepository<AccountingCompany> accountingCompaniesRepository,
            ITpkPack tpkPack,
            IBundleLabelPack bundleLabelPack,
            IWhatToWandService whatToWandService,
            IQueryRepository<SalesOutlet> salesOutletQueryRepository,
            IStoresPack storesOoPack,
            IQueryRepository<Consignment> consignmentRepository)
        {
            this.tpkView = tpkView;
            this.tpkPack = tpkPack;
            this.bundleLabelPack = bundleLabelPack;
            this.whatToWandService = whatToWandService;
            this.storesOoPack = storesOoPack;
            this.salesOutletQueryRepository = salesOutletQueryRepository;
            this.accountingCompaniesRepository = accountingCompaniesRepository;
            this.consignmentRepository = consignmentRepository;
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
            
            if (recordsToTpk != candidates.Count())
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
            
            var whatToWand = this.whatToWandService.WhatToWand(from.FromLocation);
            
            this.tpkPack.UpdateQuantityPrinted(from.FromLocation, out var updateQuantitySuccessful);
            
            if (!updateQuantitySuccessful)
            {
                throw new TpkException("Failed in update_qty_printed.");
            }
            
            this.storesOoPack.DoTpk(from.LocationId, from.PalletNumber, DateTime.Now, out var tpkSuccessful);
            
            if (!tpkSuccessful)
            {
                throw new TpkException(this.storesOoPack.GetErrorMessage());
            }

            var consignment = new Consignment 
                                  { 
                                      ConsignmentId = 383093, SalesAccountId = 65861, AddressId = 925132, Country = new Country
                                                  {
                                                      DisplayName = "United States Of America"
                                                  }
                                  };
                
            //this.consignmentRepository.FindBy(c => c.ConsignmentId == from.ConsignmentId);
            return new TpkResult 
                       {
                                         Success = true,
                                         Message = "TPK Successful",
                                         Transferred = transferredWithNotes,
                                         Report = new WhatToWandReport
                                                      {
                                                          Outlet = this.salesOutletQueryRepository.FindBy(o => o.AccountId == consignment.SalesAccountId),
                                                          Consignment = consignment,
                                                          TotalNettValueOfConsignment = 100.0m,
                                                          Type = "*START*",
                                                          Lines = new List<WhatToWandLine>
                                                                      {
                                                                          new WhatToWandLine
                                                                              {
                                                                                  OrderNumber = 603136,
                                                                                  OrderLine = 1,
                                                                                  ArticleNumber = "LINGO 4",
                                                                                  InvoiceDescription = "LINGO 4 LP12 POWER SUPPLY IN BLACK",
                                                                                  Manual = null,
                                                                                  MainsLead = "CONN 014/1"
                                                                              },
                                                                          new WhatToWandLine
                                                                              {
                                                                                  OrderNumber = 603136,
                                                                                  OrderLine = 2,
                                                                                  ArticleNumber = "LINGO 4",
                                                                                  InvoiceDescription = "LINGO 4 LP12 POWER SUPPLY IN BLACK",
                                                                                  Manual = null,
                                                                                  MainsLead = "CONN 014/1"
                                                                              },
                                                                          new WhatToWandLine
                                                                              {
                                                                                  OrderNumber = 603136,
                                                                                  OrderLine = 3,
                                                                                  ArticleNumber = "LINGO 4",
                                                                                  InvoiceDescription = "LINGO 4 LP12 POWER SUPPLY IN BLACK",
                                                                                  Manual = null,
                                                                                  MainsLead = "CONN 014/1"
                                                                              },
                                                                      }
                                                      },
                                     };
        }
    }
}
