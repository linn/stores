namespace Linn.Stores.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Allocation;

    public class SosAllocDetailResponseProcessor : JsonResponseProcessor<SosAllocDetail>
    {
        public SosAllocDetailResponseProcessor(IResourceBuilder<SosAllocDetail> resourceBuilder)
            : base(resourceBuilder, "linnapps-sos-alloc-detail", 1)
        {
        }
    }
}
