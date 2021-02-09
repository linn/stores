namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class StockPoolRepository : IRepository<StockPool, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public StockPoolRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public StockPool FindById(int key)
        {
            return this.serviceDbContext.StockPools.Where(a => a.Id == key).ToList().FirstOrDefault();
        }

        public IQueryable<StockPool> FindAll()
        {
            return this.serviceDbContext.StockPools;
        }

        public void Add(StockPool entity)
        {
            this.serviceDbContext.StockPools.Add(entity);
        }

        public void Remove(StockPool entity)
        {
            throw new NotImplementedException();
        }

        public StockPool FindBy(Expression<Func<StockPool, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<StockPool> FilterBy(Expression<Func<StockPool, bool>> expression)
        {
            return this.serviceDbContext.StockPools.Where(expression);
        }
    }
}
