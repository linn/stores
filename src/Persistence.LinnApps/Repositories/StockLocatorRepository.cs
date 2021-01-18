namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class StockLocatorRepository : IQueryRepository<StockLocator>
    {
        private readonly ServiceDbContext serviceDbContext;

        public StockLocatorRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public StockLocator FindBy(Expression<Func<StockLocator, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<StockLocator> FilterBy(Expression<Func<StockLocator, bool>> expression)
        {
            return this.serviceDbContext.StockLocators.Where(expression);
        }

        public IQueryable<StockLocator> FindAll()
        {
            return this.serviceDbContext.StockLocators;
        }
    }
}