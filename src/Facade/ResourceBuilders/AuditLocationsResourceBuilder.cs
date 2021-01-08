namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class AuditLocationsResourceBuilder : IResourceBuilder<IEnumerable<AuditLocation>>
    {
        private readonly AuditLocationResourceBuilder auditLocationResourceBuilder = new AuditLocationResourceBuilder();

        public IEnumerable<AuditLocationResource> Build(IEnumerable<AuditLocation> auditLocations)
        {
            return auditLocations.Select(a => this.auditLocationResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<AuditLocation>>.Build(IEnumerable<AuditLocation> auditLocations) =>
            this.Build(auditLocations);

        public string GetLocation(IEnumerable<AuditLocation> auditLocations)
        {
            throw new NotImplementedException();
        }
    }
}