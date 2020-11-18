using System;

namespace Linn.Stores.Domain.LinnApps.Parts
{
    public class MechPartManufacturerAlt
    {
        public int MechPartSourceId { get; set; }

        public int Sequence { get; set; }

        public string ManufacturerCode { get; set; }

        public MechPartSource MechPartSource { get; set; }

        public Manufacturer Manufacturer { get; set; }

        public int Preference { get; set; }

        public string PartNumber { get; set; }

        public string ReelSuffix { get; set; }

        public string RohsCompliant { get; set; }

        public Employee ApprovedBy { get; set; }

        public DateTime DateApproved { get; set; }
    }
}
