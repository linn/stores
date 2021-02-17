namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    public class StockLocatorBatchesRepository : IQueryRepository<StockLocatorBatch>
    {
        private readonly ServiceDbContext serviceDbContext;

        public StockLocatorBatchesRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public StockLocatorBatch FindBy(Expression<Func<StockLocatorBatch, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<StockLocatorBatch> FilterBy(Expression<Func<StockLocatorBatch, bool>> expression)
        {
            return this.serviceDbContext.StockLocatorBatchesView.Where(expression);
        }

        public IQueryable<StockLocatorBatch> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
