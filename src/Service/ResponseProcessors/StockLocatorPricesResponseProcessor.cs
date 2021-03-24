namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    public class StockLocatorPricesResponseProcessor : JsonResponseProcessor<IEnumerable<StockLocatorPrices>>
    {
        public StockLocatorPricesResponseProcessor(IResourceBuilder<IEnumerable<StockLocatorPrices>> resourceBuilder)
            : base(resourceBuilder, "stock-locator-prices", 1)
        {
        }
    }
}
