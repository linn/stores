namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps;

    public class AuditLocationsResponseProcessor : JsonResponseProcessor<IEnumerable<AuditLocation>>
    {
        public AuditLocationsResponseProcessor(IResourceBuilder<IEnumerable<AuditLocation>> resourceBuilder)
            : base(resourceBuilder, "audit-locations", 1)
        {
        }
    }
}