namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.GoodsIn;

    using Microsoft.EntityFrameworkCore;

    public class PurchaseOrderRepository : IRepository<PurchaseOrder, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public PurchaseOrderRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public void Add(PurchaseOrder entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<PurchaseOrder> FilterBy(Expression<Func<PurchaseOrder, bool>> expression)
        {
            return this.serviceDbContext.PurchaseOrders.Where(expression)
                .Include(o => o.Details.Where(x => x.Line == 1)).ThenInclude(z => z.SalesArticle)
                .ThenInclude(a => a.Tariff).AsNoTracking().AsQueryable();
        }

        public IQueryable<PurchaseOrder> FindAll()
        {
            throw new NotImplementedException();
        }

        public PurchaseOrder FindBy(Expression<Func<PurchaseOrder, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public PurchaseOrder FindById(int key)
        {
            return this.serviceDbContext.PurchaseOrders.Where(x => x.OrderNumber == key).Include(o => o.Supplier)
                .Include(o => o.Details).AsNoTracking().ToList().FirstOrDefault();
        }

        public void Remove(PurchaseOrder entity)
        {
            throw new NotImplementedException();
        }
    }
}
