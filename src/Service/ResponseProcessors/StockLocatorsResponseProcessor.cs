namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    public class StockLocatorsResponseProcessor : JsonResponseProcessor<IEnumerable<StockLocator>>
    {
        public StockLocatorsResponseProcessor(IResourceBuilder<IEnumerable<StockLocator>> resourceBuilder)
            : base(resourceBuilder, "stock-locators", 1)
        {
        }
    }
}
