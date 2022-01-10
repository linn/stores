namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    using Microsoft.EntityFrameworkCore;

    public class InvoiceDetailRepository : IRepository<InvoiceDetail, InvoiceDetailKey>
    {
        private readonly ServiceDbContext serviceDbContext;

        public InvoiceDetailRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public InvoiceDetail FindById(InvoiceDetailKey key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<InvoiceDetail> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(InvoiceDetail entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(InvoiceDetail entity)
        {
            throw new NotImplementedException();
        }

        public InvoiceDetail FindBy(Expression<Func<InvoiceDetail, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<InvoiceDetail> FilterBy(Expression<Func<InvoiceDetail, bool>> expression)
        {
            return this.serviceDbContext
                .InvoiceDetails.Where(expression)
                .Include(d => d.Invoice).ThenInclude(i => i.Account)
                .Include(d => d.Invoice).ThenInclude(i => i.DeliveryAddress);
        }
    }
}
