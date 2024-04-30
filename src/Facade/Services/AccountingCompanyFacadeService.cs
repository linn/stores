namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class AccountingCompanyFacadeService : IAccountingCompanyFacadeService
    {
        private readonly IQueryRepository<AccountingCompany> repository;

        private readonly IBuilder<AccountingCompany> resourceBuilder;

        public AccountingCompanyFacadeService(IQueryRepository<AccountingCompany> repository, IBuilder<AccountingCompany> resourceBuilder)
        {
            this.repository = repository;
            this.resourceBuilder = resourceBuilder;
        }

        public IResult<IEnumerable<AccountingCompanyResource>> GetValid()
        {
            var results = this.repository.FilterBy(c => !c.DateInvalid.HasValue);
            return new SuccessResult<IEnumerable<AccountingCompanyResource>>(
                results.Select(a => (AccountingCompanyResource) this.resourceBuilder.Build(a, new List<string>())));
        }
    }
}
