namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    public class MechPartManufacturerAltResourceBuilder : IResourceBuilder<MechPartManufacturerAlt>
    {
        public MechPartManufacturerAltResource Build(MechPartManufacturerAlt model)
        {
            return new MechPartManufacturerAltResource
            {
               Sequence = model.Sequence,
               ManufacturerCode = model.ManufacturerCode,
               ManufacturerDescription = model.Manufacturer?.Description,
               Preference = model.Preference,
               PartNumber = model.PartNumber,
               ReelSuffix = model.ReelSuffix,
               RohsCompliant = model.RohsCompliant,
               ApprovedBy = model.ApprovedBy?.Id,
               ApprovedByName = model.ApprovedBy?.FullName,
               DateApproved = model.DateApproved?.ToString("o")
            };
        }

        public string GetLocation(MechPartManufacturerAlt model)
        {
           throw new NotImplementedException();
        }

        object IResourceBuilder<MechPartManufacturerAlt>.Build(MechPartManufacturerAlt alt) => this.Build(alt);

        private IEnumerable<LinkResource> BuildLinks(MechPartManufacturerAlt source)
        {
            throw new NotImplementedException();
        }
    }
}
