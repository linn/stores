namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Resources.Consignments;

    public class CarriersResourceBuilder : IResourceBuilder<IEnumerable<Carrier>>
    {
        private readonly CarrierResourceBuilder carrierResourceBuilder = new CarrierResourceBuilder();

        public IEnumerable<CarrierResource> Build(IEnumerable<Carrier> carriers)
        {
            return carriers
                .Select(a => this.carrierResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<Carrier>>.Build(IEnumerable<Carrier> carriers) => this.Build(carriers);

        public string GetLocation(IEnumerable<Carrier> carriers)
        {
            throw new NotImplementedException();
        }
    }
}
