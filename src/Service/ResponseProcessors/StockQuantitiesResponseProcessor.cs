namespace Linn.Stores.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    public class StockQuantitiesResponseProcessor : JsonResponseProcessor<StockQuantities>
    {
        public StockQuantitiesResponseProcessor(IResourceBuilder<StockQuantities> resourceBuilder)
            : base(resourceBuilder, "stock-quantities", 1)
        {
        }
    }
}
