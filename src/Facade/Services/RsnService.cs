namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class RsnService : IRsnService
    {
        private readonly IQueryRepository<Rsn> repository;

        public RsnService(IQueryRepository<Rsn> repository)
        {
            this.repository = repository;
        }

        public IResult<IEnumerable<Rsn>> SearchRsns(int accountId, int? outletNumber)
        {
            if (outletNumber != null)
            {
                return new SuccessResult<IEnumerable<Rsn>>(
                    this.repository.FilterBy(rsn => rsn.AccountId == accountId && rsn.OutletNumber == outletNumber));
            }

            return new SuccessResult<IEnumerable<Rsn>>(this.repository.FilterBy(rsn => rsn.AccountId == accountId));
        }
    }
}