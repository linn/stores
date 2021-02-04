namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Resources;

    public interface IStockLocatorFacadeService 
        : IFacadeFilterService<StockLocator, int, StockLocatorResource, StockLocatorResource, StockLocatorResource>
    {
        IResult<StockLocator> Delete(StockLocatorResource resource);

        IResult<IEnumerable<StockLocatorWithStoragePlaceInfo>> 
            GetStockLocatorsForPart(string partNumber);

        IResult<IEnumerable<StockLocator>> GetBatches(string batchRef);
    }
}
