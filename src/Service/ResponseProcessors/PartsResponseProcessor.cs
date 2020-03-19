namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class PartsResponseProcessor : JsonResponseProcessor<IEnumerable<Part>>
    {
        public PartsResponseProcessor(IResourceBuilder<IEnumerable<Part>> resourceBuilder)
            : base(resourceBuilder, "linnapps-parts", 1)
        {
        }
    }
}