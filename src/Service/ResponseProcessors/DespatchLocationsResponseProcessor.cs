namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Allocation;

    public class DespatchLocationsResponseProcessor : JsonResponseProcessor<IEnumerable<DespatchLocation>>
    {
        public DespatchLocationsResponseProcessor(IResourceBuilder<IEnumerable<DespatchLocation>> resourceBuilder)
            : base(resourceBuilder, "linnapps-despatch-locations", 1)
        {
        }
    }
}
