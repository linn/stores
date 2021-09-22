namespace Linn.Stores.Resources.ImportBooks
{
    using Linn.Common.Resources;

    public class ImportBookInvoiceDetailResource : HypermediaResource
    {
        public int ImportBookId { get; set; }

        public int LineNumber { get; set; }

        public string InvoiceNumber { get; set; }

        public decimal InvoiceValue { get; set; }
    }
}
