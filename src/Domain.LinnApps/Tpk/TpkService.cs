namespace Linn.Stores.Domain.LinnApps.Tpk
{
    using System.Linq;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Tpk.Models;

    public class TpkService : ITpkService
    {
        private readonly IQueryRepository<TransferableStock> tpkView;

        private readonly IQueryRepository<AccountingCompany> accountingCompaniesRepository;

        private readonly ITpkOoPack tpkOoPack;

        private readonly IBundleLabelPack bundleLabelPack;

        private readonly IWhatToWandService whatToWandService;

        public TpkService(
            IQueryRepository<TransferableStock> tpkView,
            IQueryRepository<AccountingCompany> accountingCompaniesRepository,
            ITpkOoPack tpkOoPack,
            IBundleLabelPack bundleLabelPack,
            IWhatToWandService whatToWandService)
        {
            this.tpkView = tpkView;
            this.tpkOoPack = tpkOoPack;
            this.bundleLabelPack = bundleLabelPack;
            this.whatToWandService = whatToWandService;
            this.accountingCompaniesRepository = accountingCompaniesRepository;
        }

        public TpkResult TransferStock(TpkRequest tpkRequest)
        {
            var candidates = tpkRequest.StockToTransfer.ToList();
            var fromLocation = candidates.First().FromLocation;
            if (candidates.Any(s => s.FromLocation != fromLocation))
            {
                throw new TpkException("You can only TPK one pallet at a time");
            }

            var recordsToTpk = this.tpkView.FilterBy(r => r.FromLocation == fromLocation).Count();

            if (recordsToTpk != candidates.Count())
            {
                throw new TpkException("You haven't looked at everything from location " + fromLocation);
            }

            var latestAllocationDateTime = this.accountingCompaniesRepository.FindBy(c => c.Name == "LINN")
                .LatesSalesAllocationDate;

            if (tpkRequest.DateTimeTpkViewQueried < latestAllocationDateTime)
            {
                throw new TpkException("Another allocation was run at " + latestAllocationDateTime +
                ". You must re-query the TPK screen to get an up to date version.");
            }

            var transferredWithNotes = candidates.Select(
                s => new TransferredStock(s, this.tpkOoPack.GetTpkNotes((int)s.ConsignmentId, s.FromLocation)));

            this.bundleLabelPack.PrintTpkBoxLabels(fromLocation);

            var whatToWand = this.whatToWandService.WhatToWand(fromLocation);

            return new TpkResult
                       {
                           Success = true,
                           Message = "Some message",
                           Transferred = transferredWithNotes,
                           WhatToWand = whatToWand
                       };
        }
    }
}
