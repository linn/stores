namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Consignments;

    public class CarrierRepository : IRepository<Carrier, string>
    {
        private readonly ServiceDbContext serviceDbContext;

        public CarrierRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public Carrier FindById(string key)
        {
            return this.serviceDbContext.Carriers.Where(p => p.CarrierCode == key)
                .ToList().FirstOrDefault();
        }

        public IQueryable<Carrier> FindAll()
        {
            return this.serviceDbContext.Carriers.Where(c => c.DateInvalid == null);
        }

        public void Add(Carrier entity)
        {
            this.serviceDbContext.Carriers.Add(entity);
        }

        public void Remove(Carrier entity)
        {
            throw new NotImplementedException();
        }

        public Carrier FindBy(Expression<Func<Carrier, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Carrier> FilterBy(Expression<Func<Carrier, bool>> expression)
        {
            return this.serviceDbContext
                .Carriers
                .Where(expression);
        }
    }
}
