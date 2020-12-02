namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class AuditLocationResourceBuilder : IResourceBuilder<AuditLocation>
    {
        public AuditLocationResource Build(AuditLocation auditLocation)
        {
            return new AuditLocationResource { StoragePlace = auditLocation.StoragePlace };
        }

        public string GetLocation(AuditLocation model)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<AuditLocation>.Build(AuditLocation auditLocation) => this.Build(auditLocation);

        private IEnumerable<LinkResource> BuildLinks(AuditLocation supplier)
        {
            throw new NotImplementedException();
        }
    }
}