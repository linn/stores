namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Wcs;

    using Microsoft.EntityFrameworkCore;

    public class WarehouseLocationRepository : IQueryRepository<WarehouseLocation>
    {
        private readonly ServiceDbContext serviceDbContext;

        public WarehouseLocationRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext; 
        }

        public WarehouseLocation FindBy(Expression<Func<WarehouseLocation, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<WarehouseLocation> FilterBy(Expression<Func<WarehouseLocation, bool>> expression)
        {
            return this.serviceDbContext.WarehouseLocations.Where(expression).Include(l => l.Pallet);
        }

        public IQueryable<WarehouseLocation> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
