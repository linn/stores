namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    using Microsoft.EntityFrameworkCore;

    public class SalesAccountRepository : IQueryRepository<SalesAccount>
    {
        private readonly ServiceDbContext serviceDbContext;

        public SalesAccountRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public SalesAccount FindBy(Expression<Func<SalesAccount, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<SalesAccount> FilterBy(Expression<Func<SalesAccount, bool>> expression)
        {
            return this.serviceDbContext.SalesAccounts.Where(expression).AsNoTracking();
        }

        public IQueryable<SalesAccount> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}