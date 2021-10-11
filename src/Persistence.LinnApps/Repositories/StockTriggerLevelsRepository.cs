namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    using Microsoft.EntityFrameworkCore;

    public class StockTriggerLevelsRepository : IQueryRepository<StockTriggerLevel>
    {

        private readonly ServiceDbContext serviceDbContext;

        public StockTriggerLevelsRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public StockTriggerLevel FindBy(Expression<Func<StockTriggerLevel, bool>> expression)
        {
            return this.serviceDbContext.StockTriggerLevels.Where(expression).AsNoTracking().ToList().SingleOrDefault();
        }

        public IQueryable<StockTriggerLevel> FilterBy(Expression<Func<StockTriggerLevel, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<StockTriggerLevel> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
