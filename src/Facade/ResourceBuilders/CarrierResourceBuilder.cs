namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class CarrierResourceBuilder : IResourceBuilder<Carrier>
    {
        public CarrierResource Build(Carrier carrier)
        {
            return new CarrierResource
                       {
                           CarrierCode = carrier.CarrierCode,
                           Name = carrier.Name,
                           OrganisationId = carrier.OrganisationId
                       };
        }

        public string GetLocation(Carrier carrier)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<Carrier>.Build(Carrier carrier) => this.Build(carrier);

        private IEnumerable<LinkResource> BuildLinks(Carrier carrier)
        {
            throw new NotImplementedException();
        }
    }
}
