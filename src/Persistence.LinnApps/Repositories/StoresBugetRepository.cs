namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class StoresBugetRepository : IQueryRepository<StoresBudget>
    {
        private readonly ServiceDbContext serviceDbContext;

        public StoresBugetRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public StoresBudget FindBy(Expression<Func<StoresBudget, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<StoresBudget> FilterBy(Expression<Func<StoresBudget, bool>> expression)
        {
            return this.serviceDbContext.StoresBudgets.Where(expression);
        }

        public IQueryable<StoresBudget> FindAll()
        {
            return this.serviceDbContext.StoresBudgets;
        }
    }
}