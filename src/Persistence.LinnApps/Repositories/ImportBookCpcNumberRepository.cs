namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ImportBooks;

    public class ImportBookCpcNumberRepository : IRepository<ImportBookCpcNumber, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public ImportBookCpcNumberRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public ImportBookCpcNumber FindById(int key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ImportBookCpcNumber> FindAll()
        {
            return this.serviceDbContext.ImportBookCpcNumbers.Where(n => !n.DateInvalid.HasValue);
        }

        public void Add(ImportBookCpcNumber entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(ImportBookCpcNumber entity)
        {
            throw new NotImplementedException();
        }

        public ImportBookCpcNumber FindBy(Expression<Func<ImportBookCpcNumber, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ImportBookCpcNumber> FilterBy(Expression<Func<ImportBookCpcNumber, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}