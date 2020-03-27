namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Allocation;

    public class DespatchLocationRepository : IRepository<DespatchLocation, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public DespatchLocationRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public DespatchLocation FindById(int key)
        {
            return this.serviceDbContext.DespatchLocations.Where(a => a.Id == key).ToList().FirstOrDefault();
        }

        public IQueryable<DespatchLocation> FindAll()
        {
            return this.serviceDbContext.DespatchLocations;
        }

        public void Add(DespatchLocation entity)
        {
            this.serviceDbContext.DespatchLocations.Add(entity);
        }

        public void Remove(DespatchLocation entity)
        {
            throw new NotImplementedException();
        }

        public DespatchLocation FindBy(Expression<Func<DespatchLocation, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<DespatchLocation> FilterBy(Expression<Func<DespatchLocation, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}