namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class PartStorageTypeRepository : IRepository<PartStorageType, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public PartStorageTypeRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public PartStorageType FindById(int key)
        {
            return this.serviceDbContext
                .PartStorageTypes.FirstOrDefault(p => p.Id == key);
        }

        public IQueryable<PartStorageType> FindAll()
        {
            return this.serviceDbContext.PartStorageTypes;
        }

        public void Add(PartStorageType entity)
        {
            this.serviceDbContext.PartStorageTypes.Add(entity);
        }

        public void Remove(PartStorageType entity)
        {
            throw new NotImplementedException();
        }

        public PartStorageType FindBy(Expression<Func<PartStorageType, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<PartStorageType> FilterBy(Expression<Func<PartStorageType, bool>> expression)
        {
            return this.serviceDbContext
                .PartStorageTypes
                .Where(expression);
        }
    }
}
