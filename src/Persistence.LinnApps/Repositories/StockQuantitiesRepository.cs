namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    using Microsoft.EntityFrameworkCore;

    public class StockQuantitiesRepository : IFilterByWildcardQueryRepository<StockQuantities>
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

        public IQueryable<StockQuantities> FilterByWildcard(string search)
        {
            return this.serviceDbContext.StockQuantitiesForMrView
                .Where(x => EF.Functions.Like(x.PartNumber, search));
        }
    }
}
