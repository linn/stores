using System.Collections.Generic;

namespace Linn.Stores.Domain.LinnApps.Parts
{
    public class Manufacturer
    {
        public string Code { get; set; }

        public string Description { get; set; }

        public IEnumerable<MechPartManufacturerAlt> MechPartManufacturerAlts { get; set; }
    }
}
