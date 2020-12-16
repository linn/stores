namespace Linn.Stores.Domain.LinnApps.Parts
{
    public class MechPartPurchasingQuote
    {
        public int SourceId { get; set; }

        public MechPartSource Source { get; set; }

        public int LeadTime { get; set; }

        public string ManufacturersCode { get; set; }

        public string ManufacturersPartNumber { get; set; }

        public decimal? Moq { get; set; }

        public string RohsCompliant { get; set; }

        public int SupplierId { get; set; }

        public Supplier Supplier { get; set; }

        public decimal? UnitPrice { get; set; }
    }
}
