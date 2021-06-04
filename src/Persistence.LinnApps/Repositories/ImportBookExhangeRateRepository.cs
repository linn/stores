namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ImportBooks;

    public class ImportBookExhangeRateRepository : IRepository<ImportBookExchangeRate, ImportBookExchangeRateKey>
    {
        private readonly ServiceDbContext serviceDbContext;

        public ImportBookExhangeRateRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public ImportBookExchangeRate FindById(ImportBookExchangeRateKey key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ImportBookExchangeRate> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(ImportBookExchangeRate entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(ImportBookExchangeRate entity)
        {
            throw new NotImplementedException();
        }

        public ImportBookExchangeRate FindBy(Expression<Func<ImportBookExchangeRate, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ImportBookExchangeRate> FilterBy(Expression<Func<ImportBookExchangeRate, bool>> expression)
        {
            return this.serviceDbContext.ImportBookExchangeRates.Where(expression);
        }
    }
}
