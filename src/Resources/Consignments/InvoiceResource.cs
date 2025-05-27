namespace Linn.Stores.Resources.Consignments
{
    using Linn.Common.Resources;

    public class InvoiceResource : HypermediaResource
    {
        public int DocumentNumber { get; set; }

        public string DocumentType { get; set; }

        public int? ConsignmentId { get; set; }
    }
}
