namespace Linn.Stores.Resources
{
    public class ConsignmentShipfileResource
    {
        public int Id { get; set; }

        public int ConsignmentId { get; set; }

        public string DateClosed { get; set; }

        public string CustomerName { get; set; }

        public string InvoiceNumbers { get; set; }

        public string Status { get; set; }
    }
}
