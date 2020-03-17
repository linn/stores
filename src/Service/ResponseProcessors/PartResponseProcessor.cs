namespace Linn.Stores.Service.ResponseProcessors
{
    using Domain.LinnApps.Parts;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;

    public class PartResponseProcessor : JsonResponseProcessor<Part>
    {
        public PartResponseProcessor(IResourceBuilder<Part> resourceBuilder)
            : base(resourceBuilder, "linnapps-part", 1)
        {
        }
    }
}