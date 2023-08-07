namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    public class StockTriggerLevelsResponseProcessor : JsonResponseProcessor<IEnumerable<StockTriggerLevel>>
    {
        public StockTriggerLevelsResponseProcessor(IResourceBuilder<IEnumerable<StockTriggerLevel>> resourceBuilder)
            : base(resourceBuilder, "stock-trigger-levels", 1)
        {
        }
    }
}
