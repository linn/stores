namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    using Microsoft.EntityFrameworkCore;

    public class StockLocatorLocationsRepository : IQueryRepository<StockLocatorLocation>
    {
        private readonly ServiceDbContext serviceDbContext;

        public StockLocatorLocationsRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public StockLocatorLocation FindBy(Expression<Func<StockLocatorLocation, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<StockLocatorLocation> FilterBy(Expression<Func<StockLocatorLocation, bool>> expression)
        {
            return this.serviceDbContext.StockLocatorLocationsView
                .Where(expression)
                .Include(l => l.StorageLocation)
                .Include(l => l.Part)
                .AsNoTracking();
        }

        public IQueryable<StockLocatorLocation> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
