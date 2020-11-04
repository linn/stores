namespace Linn.Stores.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class PartResponseProcessor : JsonResponseProcessor<Part>
    {
        public PartResponseProcessor(IResourceBuilder<Part> resourceBuilder)
            : base(resourceBuilder, "linnapps-part", 1)
        {
        }
    }
}
