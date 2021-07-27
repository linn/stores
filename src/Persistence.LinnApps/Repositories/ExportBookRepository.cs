namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ExportBooks;

    public class ExportBookRepository : IRepository<ExportBook, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public ExportBookRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public ExportBook FindById(int key)
        {
            return this.serviceDbContext.ExportBooks.Where(exportBook => exportBook.ExportId == key)
                .ToList().FirstOrDefault();
        }

        public IQueryable<ExportBook> FindAll()
        {
            return this.serviceDbContext.ExportBooks;
        }

        public void Add(ExportBook entity)
        {
            this.serviceDbContext.ExportBooks.Add(entity);
        }

        public void Remove(ExportBook entity)
        {
            throw new NotImplementedException();
        }

        public ExportBook FindBy(Expression<Func<ExportBook, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ExportBook> FilterBy(Expression<Func<ExportBook, bool>> expression)
        {
            return this.serviceDbContext
                .ExportBooks
                .Where(expression);
        }
    }
}
