namespace Linn.Stores.Resources.Parts
{
    using Linn.Common.Resources;

    public class ImportBookInvoiceDetailResource : HypermediaResource
    {
        public int ImportBookId { get; set; }

        public int LineNumber { get; set; }

        public string InvoiceNumber { get; set; }

        public int InvoiceValue { get; set; }
    }
}