namespace Linn.Stores.Domain.LinnApps.Parts
{
    public class MechPartAlt
    {
        public int MechPartSourceId { get; set; }

        public int Sequence { get; set; }

        public Supplier Supplier { get; set; }

        public MechPartSource MechPartSource { get; set; }

        public string PartNumber { get; set; }
    }
}
