namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Consignments;

    public class HubsResponseProcessor : JsonResponseProcessor<IEnumerable<Hub>>
    {
        public HubsResponseProcessor(IResourceBuilder<IEnumerable<Hub>> resourceBuilder)
            : base(resourceBuilder, "hubs", 1)
        {
        }
    }
}
