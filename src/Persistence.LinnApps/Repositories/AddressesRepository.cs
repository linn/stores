namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;

    using Linn.Stores.Domain.LinnApps;
    using Microsoft.EntityFrameworkCore;

    public class AddressesRepository : IRepository<Address, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public AddressesRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public Address FindById(int key)
        {
            return this.serviceDbContext.Addresses.Where(c => c.Id == key).Include(a => a.Country).ToList().FirstOrDefault();
        }

        public IQueryable<Address> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(Address entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(Address entity)
        {
            throw new NotImplementedException();
        }

        public Address FindBy(Expression<Func<Address, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Address> FilterBy(Expression<Func<Address, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
