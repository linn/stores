namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Persistence.LinnApps;

    public class CountryRepository : IRepository<Country, string>
    {
        private readonly ServiceDbContext serviceDbContext;

        public CountryRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public Country FindById(string key)
        {
            return this.serviceDbContext.Countries
                .Where(c => c.CountryCode == key)
                .ToList().FirstOrDefault();
        }

        public IQueryable<Country> FindAll()
        {
            return this.serviceDbContext.Countries.Where(a => !a.DateInvalid.HasValue);
        }

        public void Add(Country entity)
        {
            this.serviceDbContext.Countries.Add(entity);
        }

        public void Remove(Country entity)
        {
            throw new NotImplementedException();
        }

        public Country FindBy(Expression<Func<Country, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Country> FilterBy(Expression<Func<Country, bool>> expression)
        {
            return this.serviceDbContext.Countries.Where(expression);
        }
    }
}
