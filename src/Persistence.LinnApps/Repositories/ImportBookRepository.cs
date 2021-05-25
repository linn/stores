namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ImportBooks;


    public class ImportBookRepository : IRepository<ImportBook, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public ImportBookRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public ImportBook FindById(int key)
        {
            return this.serviceDbContext.ImportBooks.Where(b => b.Id == key).ToList().FirstOrDefault();
        }

        public IQueryable<ImportBook> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(ImportBook entity)
        {
            this.serviceDbContext.Add(entity);
        }

        public void Remove(ImportBook entity)
        {
            throw new NotImplementedException();
        }

        public ImportBook FindBy(Expression<Func<ImportBook, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ImportBook> FilterBy(Expression<Func<ImportBook, bool>> expression)
        {
            return this.serviceDbContext
                .ImportBooks
                .Where(expression);
        }
    }
}
