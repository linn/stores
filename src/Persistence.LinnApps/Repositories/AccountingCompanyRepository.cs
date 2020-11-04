namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class AccountingCompanyRepository : IQueryRepository<AccountingCompany>
    {
        private readonly ServiceDbContext serviceDbContext;

        public AccountingCompanyRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public AccountingCompany FindBy(Expression<Func<AccountingCompany, bool>> expression)
        {
            return this.serviceDbContext.AccountingCompanies.Where(expression).ToList().FirstOrDefault();
        }

        public IQueryable<AccountingCompany> FilterBy(Expression<Func<AccountingCompany, bool>> expression)
        {
            return this.serviceDbContext.AccountingCompanies.Where(expression);
        }

        public IQueryable<AccountingCompany> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
