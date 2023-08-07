namespace Linn.Stores.Domain.LinnApps
{
    public class InvoiceDetail
    {
        public int InvoiceNumber { get; set; }

        public Invoice Invoice { get; set; }

        public int LineNumber { get; set; }

        public decimal Total { get; set; }

        public string PartNumber { get; set; }

        public string PartDescription { get; set; }

        public string CountryOfOrigin { get; set; }
    }
}
