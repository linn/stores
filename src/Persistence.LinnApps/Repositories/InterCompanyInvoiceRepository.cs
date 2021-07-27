namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class InterCompanyInvoiceRepository : IRepository<InterCompanyInvoice, InterCompanyInvoiceKey>
    {
        private ServiceDbContext serviceDbContext;

        public InterCompanyInvoiceRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public InterCompanyInvoice FindBy(Expression<Func<InterCompanyInvoice, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<InterCompanyInvoice> FilterBy(Expression<Func<InterCompanyInvoice, bool>> expression)
        {
            return this.serviceDbContext.IntercompanyInvoices.Where(expression);
        }

        public IQueryable<InterCompanyInvoice> FindAll()
        {
            throw new NotImplementedException();
        }

        public InterCompanyInvoice FindById(InterCompanyInvoiceKey key)
        {
            throw new NotImplementedException();
        }

        public void Add(InterCompanyInvoice entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(InterCompanyInvoice entity)
        {
            throw new NotImplementedException();
        }
    }
}