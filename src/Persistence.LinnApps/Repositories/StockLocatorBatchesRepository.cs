namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    public class StockLocatorBatchesRepository : IQueryRepository<StockLocatorBatchesViewModel>
    {
        private readonly ServiceDbContext serviceDbContext;

        public StockLocatorBatchesRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public StockLocatorBatchesViewModel FindBy(Expression<Func<StockLocatorBatchesViewModel, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<StockLocatorBatchesViewModel> FilterBy(Expression<Func<StockLocatorBatchesViewModel, bool>> expression)
        {
            return this.serviceDbContext.StockLocatorBatchesView.Where(expression);
        }

        public IQueryable<StockLocatorBatchesViewModel> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
