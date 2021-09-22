namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    public class StockMovesResponseProcessor : JsonResponseProcessor<IEnumerable<StockMove>>
    {
        public StockMovesResponseProcessor(IResourceBuilder<IEnumerable<StockMove>> resourceBuilder)
            : base(resourceBuilder, "stock-moves", 1)
        {
        }
    }
}
