namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Resources.StockLocators;

    public interface IStockLocatorFacadeService 
        : IFacadeFilterService<StockLocator, int, StockLocatorResource, StockLocatorResource, StockLocatorResource>
    {
        IResult<StockLocator> Delete(StockLocatorResource resource);

        IResult<IEnumerable<StockLocatorWithStoragePlaceInfo>> 
            GetStockLocatorsForPart(int partId);

        IResult<IEnumerable<StockLocator>> GetBatches(string batchRef);

        IResult<IEnumerable<StockLocator>> GetStockLocations(StockLocatorQueryResource searchResource);

        IResult<IEnumerable<StockMove>> GetMoves(string partNumber, int? palletNumber, int? locationId);

        IResult<IEnumerable<StockLocator>> GetBatchesInRotationOrderByPart(string partSearch);
    }
}
