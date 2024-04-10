namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class InvoiceRepository : IRepository<Invoice, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public InvoiceRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public Invoice FindById(int key)
        {
            return this.serviceDbContext.Invoices.Where(c => c.DocumentNumber == key && c.DocumentType == "I").ToList().FirstOrDefault();
        }

        public IQueryable<Invoice> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(Invoice entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(Invoice entity)
        {
            throw new NotImplementedException();
        }

        public Invoice FindBy(Expression<Func<Invoice, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Invoice> FilterBy(Expression<Func<Invoice, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
