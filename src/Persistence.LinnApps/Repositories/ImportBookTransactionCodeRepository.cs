namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ImportBooks;

    public class ImportBookTransactionCodeRepository : IRepository<ImportBookTransactionCode, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public ImportBookTransactionCodeRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public ImportBookTransactionCode FindById(int key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ImportBookTransactionCode> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(ImportBookTransactionCode entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(ImportBookTransactionCode entity)
        {
            throw new NotImplementedException();
        }

        public ImportBookTransactionCode FindBy(Expression<Func<ImportBookTransactionCode, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ImportBookTransactionCode> FilterBy(
            Expression<Func<ImportBookTransactionCode, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
