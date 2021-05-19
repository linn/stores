namespace Linn.Stores.Domain.LinnApps.ImportBooks
{
    public class ImportBookInvoiceDetail
    {
        public int ImportBookId { get; set; }

        public int LineNumber { get; set; }

        public string InvoiceNumber { get; set; }

        public decimal InvoiceValue { get; set; }
    }
}