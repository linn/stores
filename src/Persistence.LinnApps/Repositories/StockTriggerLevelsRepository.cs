namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Microsoft.EntityFrameworkCore;

    public class StockTriggerLevelsRepository : IStockTriggerLevelsRepository
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
            return this.serviceDbContext.StockTriggerLevels.Where(expression);
        }

        public IQueryable<StockTriggerLevel> FindAll()
        {
            return this.serviceDbContext.StockTriggerLevels.AsNoTracking();
        }

        public StockTriggerLevel FindById(int key)
        {
            return this.serviceDbContext.StockTriggerLevels.Where(stockTriggerLevels => stockTriggerLevels.LocationId == key)
                .ToList().FirstOrDefault();
        }

        public void Add(StockTriggerLevel entity)
        {
            this.serviceDbContext.StockTriggerLevels.Add(entity);
        }

        public void Remove(StockTriggerLevel entity)
        {
            this.serviceDbContext.StockTriggerLevels.Remove(entity);
        }

        public IEnumerable<StockTriggerLevel> SearchStockTriggerLevelsWithWildCard(
            string partNumberSearchTerm,
            string storagePlaceSearchTerm)
        {
            var result = this.serviceDbContext.StockTriggerLevels.AsNoTracking().Where(
                x => (string.IsNullOrEmpty(partNumberSearchTerm) || EF.Functions.Like(
                          x.PartNumber,
                          $"{partNumberSearchTerm.Replace("*", "%")}"))
                     && (string.IsNullOrEmpty(storagePlaceSearchTerm) || EF.Functions.Like(
                             x.PalletNumber.ToString(),
                             $"{storagePlaceSearchTerm.Replace("*", "%")}")));

            return result;
        }
    }
}
