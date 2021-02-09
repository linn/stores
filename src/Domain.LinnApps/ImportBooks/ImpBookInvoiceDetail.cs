namespace Linn.Stores.Domain.LinnApps.ImportBooks
{
    public class ImpBookInvoiceDetail
    {
        public int ImpBookId { get; set; }

        public int LineNumber { get; set; }

        public string InvoiceNumber { get; set; }

        public int InvoiceValue { get; set; }
    }
}