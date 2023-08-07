namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;
    using System.Linq;
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

        public IEnumerable<ExportRsn> FindMatchingRSNs(IEnumerable<ExportRsn> rsns, string searchTerm)
        {
            // fix for ticket 14895 search term coming in as null from Adam's export rsns lookup 
            if ((string.IsNullOrEmpty(searchTerm)) || (searchTerm == "null"))
            {
                return rsns;
            }

            return rsns.ToList().Where(r => r.RsnNumber.ToString().Contains(searchTerm));
        }

        public IResult<IEnumerable<ExportRsn>> SearchRsns(int accountId, int? outletNumber, string searchTerm, string hasExportReturn)
        {
            if (outletNumber != null)
            {
                return new SuccessResult<IEnumerable<ExportRsn>>(
                    this.FindMatchingRSNs(this.repository.FilterBy(rsn => rsn.AccountId == accountId && rsn.OutletNumber == outletNumber && rsn.HasExportReturn == hasExportReturn), searchTerm));
            }

            return new SuccessResult<IEnumerable<ExportRsn>>(
                this.FindMatchingRSNs(this.repository.FilterBy(rsn => rsn.AccountId == accountId && rsn.HasExportReturn == hasExportReturn),searchTerm));
        }


    }
}
