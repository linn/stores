namespace Linn.Stores.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class MechPartSourceResponseProcessor : JsonResponseProcessor<MechPartSource>
    {
        public MechPartSourceResponseProcessor(IResourceBuilder<MechPartSource> resourceBuilder) 
            : base(resourceBuilder, "linnapps-mech-part-source-1")
        {
        }
    }
}
