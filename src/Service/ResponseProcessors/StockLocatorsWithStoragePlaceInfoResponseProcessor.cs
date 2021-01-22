namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    public class StockLocatorsWithStoragePlaceInfoResponseProcessor : JsonResponseProcessor<IEnumerable<StockLocatorWithStoragePlaceInfo>>
    {
        public StockLocatorsWithStoragePlaceInfoResponseProcessor(IResourceBuilder<IEnumerable<StockLocatorWithStoragePlaceInfo>> resourceBuilder)
            : base(resourceBuilder, "stock-locators-storage-info", 1)
        {
        }
    }
}
