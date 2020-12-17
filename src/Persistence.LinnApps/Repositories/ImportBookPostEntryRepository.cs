namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class ImportBookPostEntryRepository : IRepository<ImpBookPostEntry, ImportBookPostEntryKey>
    {
        private readonly ServiceDbContext serviceDbContext;

        public ImportBookPostEntryRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public ImpBookPostEntry FindById(ImportBookPostEntryKey key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ImpBookPostEntry> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(ImpBookPostEntry entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(ImpBookPostEntry entity)
        {
            throw new NotImplementedException();
        }

        public ImpBookPostEntry FindBy(Expression<Func<ImpBookPostEntry, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ImpBookPostEntry> FilterBy(Expression<Func<ImpBookPostEntry, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}