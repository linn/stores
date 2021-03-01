namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Models;

    public class ExportRsnService : IExportRsnService
    {
        private readonly IQueryRepository<ExportRsn> repository;

        private readonly IExportReturnsPack exportReturnsPack;

        public ExportRsnService(IQueryRepository<ExportRsn> repository, IExportReturnsPack exportReturnsPack)
        {
            this.repository = repository;
            this.exportReturnsPack = exportReturnsPack;
        }
        
        public IResult<IEnumerable<ExportRsn>> SearchRsns(int accountId, int? outletNumber)
        {
            if (outletNumber != null)
            {
                return new SuccessResult<IEnumerable<ExportRsn>>(
                    this.repository.FilterBy(rsn => rsn.AccountId == accountId && rsn.OutletNumber == outletNumber));
            }

            return new SuccessResult<IEnumerable<ExportRsn>>(this.repository.FilterBy(rsn => rsn.AccountId == accountId));
        }

        public IResult<MakeExportReturnResult> MakeExportReturn(IEnumerable<int> rsns, bool hubReturn)
        {
            return new SuccessResult<MakeExportReturnResult>(
                this.exportReturnsPack.MakeExportReturn(string.Join(",", rsns), hubReturn ? "Y" : "N"));   
        }
    }
}