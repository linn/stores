namespace Linn.Stores.Domain.LinnApps
{
    public class Invoice
    {
        public int DocumentNumber { get; set; }

        public string DocumentType { get; set; }

        public int? ConsignmentId { get; set; }

        public Consignment Consignment { get; set; }
    }
}
