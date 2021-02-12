namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    using Microsoft.EntityFrameworkCore;

    public class StockLocatorLocationsRepository : IQueryRepository<StockLocatorLocationsViewModel>
    {
        private readonly ServiceDbContext serviceDbContext;

        public StockLocatorLocationsRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public StockLocatorLocationsViewModel FindBy(Expression<Func<StockLocatorLocationsViewModel, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<StockLocatorLocationsViewModel> FilterBy(Expression<Func<StockLocatorLocationsViewModel, bool>> expression)
        {
            return this.serviceDbContext.StockLocatorLocationsView
                .Where(expression)
                .Include(l => l.StorageLocation);
        }

        public IQueryable<StockLocatorLocationsViewModel> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
