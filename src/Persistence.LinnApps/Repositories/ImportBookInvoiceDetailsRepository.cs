namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class ImportBookInvoiceDetailsRepository : IRepository<ImpBookInvoiceDetail, ImportBookInvoiceDetailKey>
    {
        private readonly ServiceDbContext serviceDbContext;

        public ImportBookInvoiceDetailsRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public ImpBookInvoiceDetail FindById(ImportBookInvoiceDetailKey key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ImpBookInvoiceDetail> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(ImpBookInvoiceDetail entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(ImpBookInvoiceDetail entity)
        {
            throw new NotImplementedException();
        }

        public ImpBookInvoiceDetail FindBy(Expression<Func<ImpBookInvoiceDetail, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ImpBookInvoiceDetail> FilterBy(Expression<Func<ImpBookInvoiceDetail, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}