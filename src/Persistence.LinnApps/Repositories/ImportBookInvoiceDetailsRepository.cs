namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ImportBooks;


    public class ImportBookInvoiceDetailsRepository : IRepository<ImportBookInvoiceDetail, ImportBookInvoiceDetailKey>
    {
        private readonly ServiceDbContext serviceDbContext;

        public ImportBookInvoiceDetailsRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public ImportBookInvoiceDetail FindById(ImportBookInvoiceDetailKey key)
        {
            return this.serviceDbContext.ImportBookInvoiceDetails.Find(key.ImportBookId, key.LineNumber);
        }

        public IQueryable<ImportBookInvoiceDetail> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(ImportBookInvoiceDetail entity)
        {
            this.serviceDbContext.ImportBookInvoiceDetails.Add(entity);
        }

        public void Remove(ImportBookInvoiceDetail entity)
        {
            throw new NotImplementedException();
        }

        public ImportBookInvoiceDetail FindBy(Expression<Func<ImportBookInvoiceDetail, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ImportBookInvoiceDetail> FilterBy(Expression<Func<ImportBookInvoiceDetail, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
