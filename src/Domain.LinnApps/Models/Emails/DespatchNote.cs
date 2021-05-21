namespace Linn.Stores.Domain.LinnApps.Models.Emails
{
    public class DespatchNote
    {
        public string ConsignmentId { get; set; }

        public string DocNumber { get; set; }

        public string DocumentDate { get; set; }

        public string InvoiceLine { get; set; }

        public string Description { get; set; }

        public string Quantity { get; set; }

        public string CustomersOrderNumbers { get; set; }

        public string SalesOrderNumber { get; set; }

        public string OrderLine { get; set; }

        public string[] SerialNumbers { get; set; }
    }
}
