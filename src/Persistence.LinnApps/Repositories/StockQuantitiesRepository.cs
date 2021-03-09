namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    public class StockQuantitiesRepository : IQueryRepository<StockQuantities>
    {
        private readonly ServiceDbContext serviceDbContext;

        public StockQuantitiesRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public StockQuantities FindBy(Expression<Func<StockQuantities, bool>> expression)
        {
            return this.serviceDbContext
                .StockQuantitiesForMrView
                .Where(expression).ToList()
                .FirstOrDefault();
        }

        public IQueryable<StockQuantities> FilterBy(Expression<Func<StockQuantities, bool>> expression)
        {
            return this.serviceDbContext
                .StockQuantitiesForMrView
                .Where(expression);
        }

        public IQueryable<StockQuantities> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
