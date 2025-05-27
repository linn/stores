namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class MechPartSourcesResponseProcessor : JsonResponseProcessor<IEnumerable<MechPartSource>>
    {
        public MechPartSourcesResponseProcessor(IResourceBuilder<IEnumerable<MechPartSource>> resourceBuilder)
            : base(resourceBuilder, "part-sources", 1)
        {
        }
    }
}
