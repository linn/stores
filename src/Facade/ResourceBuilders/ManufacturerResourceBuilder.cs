namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    public class ManufacturerResourceBuilder : IResourceBuilder<Manufacturer>
    {
        public ManufacturerResource Build(Manufacturer manufacturer)
        {
            return new ManufacturerResource
            {
                Code = manufacturer.Code,
                Description = manufacturer.Description,
            };
        }

        public string GetLocation(Manufacturer manufacturer)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<Manufacturer>.Build(Manufacturer manufacturer) => this.Build(manufacturer);

        private IEnumerable<LinkResource> BuildLinks(Manufacturer Manufacturer)
        {
            throw new NotImplementedException();
        }
    }
}
