namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.ExternalServices;

    using Microsoft.EntityFrameworkCore;

    public class StoresBudgetPostingRepository : IRepository<StoresBudgetPosting, StoresBudgetPostingKey>
    {
        private readonly ServiceDbContext serviceDbContext;

        public StoresBudgetPostingRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public StoresBudgetPosting FindById(StoresBudgetPostingKey key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<StoresBudgetPosting> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(StoresBudgetPosting entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(StoresBudgetPosting entity)
        {
            throw new NotImplementedException();
        }

        public StoresBudgetPosting FindBy(Expression<Func<StoresBudgetPosting, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<StoresBudgetPosting> FilterBy(Expression<Func<StoresBudgetPosting, bool>> expression)
        {
            return this.serviceDbContext
                .StoresBudgetPostings
                .Where(expression)
                .Include(a => a.NominalAccount)
                .ThenInclude(b => b.Department)
                .Include(a => a.NominalAccount)
                .ThenInclude(b => b.Nominal);
        }
    }
}
