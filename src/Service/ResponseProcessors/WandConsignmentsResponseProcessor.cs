namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Wand.Models;

    public class WandConsignmentsResponseProcessor : JsonResponseProcessor<IEnumerable<WandConsignment>>
    {
        public WandConsignmentsResponseProcessor(IResourceBuilder<IEnumerable<WandConsignment>> resourceBuilder)
            : base(resourceBuilder, "linnapps-wand-consignments", 1)
        {
        }
    }
}
