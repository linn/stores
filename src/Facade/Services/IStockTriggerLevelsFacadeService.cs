namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Resources;

    public interface IStockTriggerLevelsFacadeService : IFacadeService<StockTriggerLevel, int, StockTriggerLevelsResource, StockTriggerLevelsResource>
    {
        IResult<StockTriggerLevel> DeleteStockTriggerLevel(int id, int userNumber);

        IResult<IEnumerable<StockTriggerLevel>> SearchStockTriggerLevelsWithWildcard(
            string partNumberSearch,
            string storagePlaceSearch);
    }
}
