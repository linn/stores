namespace Linn.Stores.Resources.Parts
{
    public class MechPartPurchasingQuoteResource
    {
        public int SourceId { get; set; }

        public int LeadTime { get; set; }

        public string ManufacturersCode { get; set; }

        public string ManufacturersPartNumber { get; set; }

        public decimal? Moq { get; set; }

        public string RohsCompliant { get; set; }

        public int SupplierId { get; set; }

        public string SupplierName { get; set; }

        public decimal? UnitPrice { get; set; }
    }
}
