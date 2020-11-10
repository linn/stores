namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Allocation;

    public class SosAllocDetailsResponseProcessor : JsonResponseProcessor<IEnumerable<SosAllocDetail>>
    {
        public SosAllocDetailsResponseProcessor(IResourceBuilder<IEnumerable<SosAllocDetail>> resourceBuilder)
            : base(resourceBuilder, "linnapps-sos-alloc-details", 1)
        {
        }
    }
}
