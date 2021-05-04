namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class SalesOrderRepository : IQueryRepository<SalesOrder>
    {
        private readonly ServiceDbContext serviceDbContext;

        public SalesOrderRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public SalesOrder FindBy(Expression<Func<SalesOrder, bool>> expression)
        {
            return this.serviceDbContext.SalesOrders.Where(expression).ToList().FirstOrDefault();
        }

        public IQueryable<SalesOrder> FilterBy(Expression<Func<SalesOrder, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<SalesOrder> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
