namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.StockMove.Models;

    public class StockAvailableRepository : IQueryRepository<AvailableStock>
    {
        private readonly ServiceDbContext serviceDbContext;

        public StockAvailableRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public AvailableStock FindBy(Expression<Func<AvailableStock, bool>> expression)
        {
            return this.serviceDbContext
                .StockAvailable
                .Where(expression).ToList()
                .FirstOrDefault();
        }

        public IQueryable<AvailableStock> FilterBy(Expression<Func<AvailableStock, bool>> expression)
        {
            return this.serviceDbContext
                .StockAvailable
                .Where(expression);
        }

        public IQueryable<AvailableStock> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
