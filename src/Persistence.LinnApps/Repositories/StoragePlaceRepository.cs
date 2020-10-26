namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class StoragePlaceRepository : IQueryRepository<StoragePlace>
    {
        private readonly ServiceDbContext serviceDbContext;

        public StoragePlaceRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public StoragePlace FindBy(Expression<Func<StoragePlace, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<StoragePlace> FilterBy(Expression<Func<StoragePlace, bool>> expression)
        {
            return this.serviceDbContext.StoragePlaces.Where(expression);
        }

        public IQueryable<StoragePlace> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}