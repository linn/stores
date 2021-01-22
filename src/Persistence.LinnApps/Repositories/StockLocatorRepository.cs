namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    public class StockLocatorRepository : IRepository<StockLocator, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public StockLocatorRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public void Remove(StockLocator entity)
        {
            this.serviceDbContext.StockLocators.Remove(entity);
            this.serviceDbContext.SaveChanges();
        }

        public StockLocator FindBy(Expression<Func<StockLocator, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<StockLocator> FilterBy(Expression<Func<StockLocator, bool>> expression)
        {
            return this.serviceDbContext.StockLocators.Where(expression);
        }

        public StockLocator FindById(int key)
        {
            return this.serviceDbContext.StockLocators
                .Where(s => s.Id == key).ToList().FirstOrDefault();
        }

        public IQueryable<StockLocator> FindAll()
        {
            return this.serviceDbContext.StockLocators;
        }

        public void Add(StockLocator entity)
        {
            throw new NotImplementedException();
        }
    }
}
