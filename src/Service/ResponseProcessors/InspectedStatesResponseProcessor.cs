namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    public class InspectedStatesResponseProcessor : JsonResponseProcessor<IEnumerable<InspectedState>>
    {
        public InspectedStatesResponseProcessor(IResourceBuilder<IEnumerable<InspectedState>> resourceBuilder)
            : base(resourceBuilder, "inspected-states", 1)
        {
        }
    }
}
