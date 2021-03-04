namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class SalesAccountService : ISalesAccountService
    {
        private readonly IQueryRepository<SalesAccount> repository;

        public SalesAccountService(IQueryRepository<SalesAccount> repository)
        {
            this.repository = repository;
        }

        public IResult<IEnumerable<SalesAccount>> SearchSalesAccounts(string searchTerm)
        {
            var result = this.repository.FilterBy(s => s.AccountName.Contains(searchTerm.ToUpper()));

            return new SuccessResult<IEnumerable<SalesAccount>>(result);
        }
    }
}