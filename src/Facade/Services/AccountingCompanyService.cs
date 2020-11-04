namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class AccountingCompanyService : IAccountingCompanyService
    {
        private readonly IQueryRepository<AccountingCompany> repository;

        public AccountingCompanyService(IQueryRepository<AccountingCompany> repository)
        {
            this.repository = repository;
        }

        public IResult<IEnumerable<AccountingCompany>> GetValid()
        {
            return new SuccessResult<IEnumerable<AccountingCompany>>(
                this.repository.FilterBy(c => !c.DateInvalid.HasValue));
        }
    }
}
