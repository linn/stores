namespace Linn.Stores.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    public class StockLocatorResponseProcessor : JsonResponseProcessor<StockLocator>
    {
        public StockLocatorResponseProcessor(IResourceBuilder<StockLocator> resourceBuilder)
            : base(resourceBuilder, "stock-locator", 1)
        {
        }
    }
}
