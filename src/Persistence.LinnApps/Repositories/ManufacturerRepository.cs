namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class ManufacturerRepository : IRepository<Manufacturer, string>
    {
        private readonly ServiceDbContext serviceDbContext;

        public ManufacturerRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public Manufacturer FindById(string key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Manufacturer> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(Manufacturer entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(Manufacturer entity)
        {
            throw new NotImplementedException();
        }

        public Manufacturer FindBy(Expression<Func<Manufacturer, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Manufacturer> FilterBy(Expression<Func<Manufacturer, bool>> expression)
        {
            return this.serviceDbContext.Manufacturers.Where(expression);
        }
    }
}
