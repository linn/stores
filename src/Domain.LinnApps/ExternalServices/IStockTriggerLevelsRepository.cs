namespace Linn.Stores.Domain.LinnApps.ExternalServices
{
    using System.Collections.Generic;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    public interface IStockTriggerLevelsRepository : IRepository<StockTriggerLevel, int>
    {
        IEnumerable<StockTriggerLevel> SearchStockTriggerLevelsWithWildCard(
            string partNumberSearchTerm, 
            string storagePlaceSearchTerm,
            bool newestFirst = false,
            int? limit = null);
    }
}
