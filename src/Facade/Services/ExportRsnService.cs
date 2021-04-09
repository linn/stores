namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class ExportRsnService : IExportRsnService
    {
        private readonly IQueryRepository<ExportRsn> repository;

        public ExportRsnService(IQueryRepository<ExportRsn> repository)
        {
            this.repository = repository;
        }

        public IResult<IEnumerable<ExportRsn>> SearchRsns(int accountId, int? outletNumber)
        {
            if (outletNumber != null)
            {
                return new SuccessResult<IEnumerable<ExportRsn>>(
                    this.repository.FilterBy(rsn => rsn.AccountId == accountId && rsn.OutletNumber == outletNumber));
            }

            return new SuccessResult<IEnumerable<ExportRsn>>(
                this.repository.FilterBy(rsn => rsn.AccountId == accountId));
        }

    }
}