namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class CurrencyRepository : IRepository<Currency, string>
    {
        private readonly ServiceDbContext serviceDbContext;

        public CurrencyRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public Currency FindById(string key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Currency> FindAll()
        {
            return this.serviceDbContext.Currencies.Where(x => true);
        }

        public void Add(Currency entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(Currency entity)
        {
            throw new NotImplementedException();
        }

        public Currency FindBy(Expression<Func<Currency, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Currency> FilterBy(Expression<Func<Currency, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
