namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.StockMove.Models;

    public class StockAvailableRepository : IQueryRepository<StockAvailable>
    {
        private readonly ServiceDbContext serviceDbContext;

        public StockAvailableRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public StockAvailable FindBy(Expression<Func<StockAvailable, bool>> expression)
        {
            return this.serviceDbContext
                .StockAvailable
                .Where(expression).ToList()
                .FirstOrDefault();
        }

        public IQueryable<StockAvailable> FilterBy(Expression<Func<StockAvailable, bool>> expression)
        {
            return this.serviceDbContext
                .StockAvailable
                .Where(expression);
        }

        public IQueryable<StockAvailable> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
