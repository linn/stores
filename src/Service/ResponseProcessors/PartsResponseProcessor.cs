namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Domain.LinnApps.Parts;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;

    public class PartsResponseProcessor : JsonResponseProcessor<IEnumerable<Part>>
    {
        public PartsResponseProcessor(IResourceBuilder<IEnumerable<Part>> resourceBuilder)
            : base(resourceBuilder, "linnapps-parts", 1)
        {
        }
    }
}