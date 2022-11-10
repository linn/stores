namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    using Microsoft.EntityFrameworkCore;

    public class SalesOrderRepository : IQueryRepository<SalesOrder>
    {
        private readonly ServiceDbContext serviceDbContext;

        public SalesOrderRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public SalesOrder FindBy(Expression<Func<SalesOrder, bool>> expression)
        {
            return this.serviceDbContext.SalesOrders.Where(expression).FirstOrDefault();
        }

        public IQueryable<SalesOrder> FilterBy(Expression<Func<SalesOrder, bool>> expression)
        {
            var result = this.serviceDbContext.SalesOrders
                .Where(expression)
                .Include(o => o.ConsignmentItems)
                .Include(o => o.Account).ThenInclude(a => a.ContactDetails)
                .Include(o => o.SalesOutlet).ThenInclude(o => o.OrderContact)
                .AsNoTracking();
            return result;
        }

        public IQueryable<SalesOrder> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
