namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.StockMove.Models;

    public class StockAvailableResponseProcessor : JsonResponseProcessor<IEnumerable<StockAvailable>>
    {
        public StockAvailableResponseProcessor(IResourceBuilder<IEnumerable<StockAvailable>> resourceBuilder)
            : base(resourceBuilder, "linnapps-stock-available-for-move", 1)
        {
        }
    }
}
