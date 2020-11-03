namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Allocation;

    public class SosAllocHeadsResponseProcessor : JsonResponseProcessor<IEnumerable<SosAllocHead>>
    {
        public SosAllocHeadsResponseProcessor(IResourceBuilder<IEnumerable<SosAllocHead>> resourceBuilder)
            : base(resourceBuilder, "linnapps-sos-alloc-heads", 1)
        {
        }
    }
}