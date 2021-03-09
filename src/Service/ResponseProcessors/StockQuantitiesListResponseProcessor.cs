namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    public class StockQuantitiesListResponseProcessor : JsonResponseProcessor<IEnumerable<StockQuantities>>
    {
        public StockQuantitiesListResponseProcessor(IResourceBuilder<IEnumerable<StockQuantities>> resourceBuilder)
            : base(resourceBuilder, "stock-quantities-list", 1)
        {
        }
    }
}
