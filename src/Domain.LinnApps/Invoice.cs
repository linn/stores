namespace Linn.Stores.Domain.LinnApps
{
    using Linn.Stores.Domain.LinnApps.Consignments;

    public class Invoice
    {
        public int DocumentNumber { get; set; }

        public string DocumentType { get; set; }

        public int? ConsignmentId { get; set; }

        public Consignment Consignment { get; set; }
    }
}
