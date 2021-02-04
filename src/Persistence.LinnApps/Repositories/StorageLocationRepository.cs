namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    using Microsoft.EntityFrameworkCore;

    public class StorageLocationRepository : IRepository<StorageLocation, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public StorageLocationRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public StorageLocation FindById(int key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<StorageLocation> FindAll()
        {
            return this.serviceDbContext.StorageLocations.AsNoTracking();
        }

        public void Add(StorageLocation entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(StorageLocation entity)
        {
            throw new NotImplementedException();
        }

        public StorageLocation FindBy(Expression<Func<StorageLocation, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<StorageLocation> FilterBy(Expression<Func<StorageLocation, bool>> expression)
        {
            return this.serviceDbContext.StorageLocations.Where(expression);
        }
    }
}
