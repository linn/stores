namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    public class ManufacturersResourceBuilder : IResourceBuilder<IEnumerable<Manufacturer>>
    {
        private readonly ManufacturerResourceBuilder manufacturerResourceBuilder = new ManufacturerResourceBuilder();

        public IEnumerable<ManufacturerResource> Build(IEnumerable<Manufacturer> manufacturers)
        {
            return manufacturers
                .Select(a => this.manufacturerResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<Manufacturer>>.Build(IEnumerable<Manufacturer> manufacturers) => this.Build(manufacturers);

        public string GetLocation(IEnumerable<Manufacturer> manufacturers)
        {
            throw new NotImplementedException();
        }
    }
}
