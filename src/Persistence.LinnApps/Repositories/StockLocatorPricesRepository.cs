namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    public class StockLocatorPricesRepository : IQueryRepository<StockLocatorPrices>
    {
        private readonly ServiceDbContext serviceDbContext;

        public StockLocatorPricesRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public StockLocatorPrices FindBy(Expression<Func<StockLocatorPrices, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<StockLocatorPrices> FilterBy(Expression<Func<StockLocatorPrices, bool>> expression)
        {
            return this.serviceDbContext.StockLocatorView.Where(expression);
        }

        public IQueryable<StockLocatorPrices> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
