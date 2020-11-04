namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    using Microsoft.EntityFrameworkCore;

    public class NominalAccountRepository : IQueryRepository<NominalAccount>
    {
        private readonly ServiceDbContext serviceDbContext;

        public NominalAccountRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public NominalAccount FindBy(Expression<Func<NominalAccount, bool>> expression)
        {
            return this.serviceDbContext.NominalAccounts.Where(expression)
                .Include(a => a.Department).ToList().FirstOrDefault();
        }

        public IQueryable<NominalAccount> FilterBy(Expression<Func<NominalAccount, bool>> expression)
        {
            return this.serviceDbContext.NominalAccounts
                .Where(expression).Include(a => a.Nominal).Include(a => a.Department);
        }

        public IQueryable<NominalAccount> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
