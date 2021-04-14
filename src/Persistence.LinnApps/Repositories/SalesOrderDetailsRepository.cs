namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class SalesOrderDetailsRepository : IQueryRepository<SalesOrderDetail>
    {
        private readonly ServiceDbContext serviceDbContext;

        public SalesOrderDetailsRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public SalesOrderDetail FindBy(Expression<Func<SalesOrderDetail, bool>> expression)
        {
            return this.serviceDbContext.SalesOrderDetails.Where(expression).ToList().FirstOrDefault();
        }

        public IQueryable<SalesOrderDetail> FilterBy(Expression<Func<SalesOrderDetail, bool>> expression)
        {
            return this.serviceDbContext.SalesOrderDetails.Where(expression);
        }

        public IQueryable<SalesOrderDetail> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
