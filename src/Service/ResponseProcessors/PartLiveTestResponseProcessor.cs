namespace Linn.Stores.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class PartLiveTestResponseProcessor : JsonResponseProcessor<PartLiveTest>
    {
        public PartLiveTestResponseProcessor(IResourceBuilder<PartLiveTest> resourceBuilder)
            : base(resourceBuilder, "linnapps-part-live-test", 1)
        {
        }
    }
}