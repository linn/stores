namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ImportBooks;


    public class ImportBookPostEntryRepository : IRepository<ImportBookPostEntry, ImportBookPostEntryKey>
    {
        private readonly ServiceDbContext serviceDbContext;

        public ImportBookPostEntryRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public ImportBookPostEntry FindById(ImportBookPostEntryKey key)
        {
            return this.serviceDbContext.ImportBookPostEntries.Find(key.ImportBookId, key.LineNumber);
        }

        public IQueryable<ImportBookPostEntry> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(ImportBookPostEntry entity)
        {
            this.serviceDbContext.ImportBookPostEntries.Add(entity);
        }

        public void Remove(ImportBookPostEntry entity)
        {
            throw new NotImplementedException();
        }

        public ImportBookPostEntry FindBy(Expression<Func<ImportBookPostEntry, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ImportBookPostEntry> FilterBy(Expression<Func<ImportBookPostEntry, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
