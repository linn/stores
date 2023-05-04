namespace Linn.Stores.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Resources;

    public interface IStockTriggerLevelsFacadeService : IFacadeService<StockTriggerLevel, int, StockTriggerLevelsResource, StockTriggerLevelsResource>
    {
        IResult<StockTriggerLevel> DeleteStockTriggerLevel(int id);
    }
}
