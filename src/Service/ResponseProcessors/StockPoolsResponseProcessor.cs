namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps;

    public class StockPoolsResponseProcessor : JsonResponseProcessor<IEnumerable<StockPool>>
    {
        public StockPoolsResponseProcessor(IResourceBuilder<IEnumerable<StockPool>> resourceBuilder)
            : base(resourceBuilder, "linnapps-stock-pools", 1)
        {
        }
    }
}