namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    public class StockTriggerLevelResponseProcessor : JsonResponseProcessor<StockTriggerLevel>
    {
        public StockTriggerLevelResponseProcessor(IResourceBuilder<StockTriggerLevel> resourceBuilder)
            : base(resourceBuilder, "stock-trigger-level", 1)
        {
        }
    }
}
