namespace Linn.Stores.Domain.LinnApps.Models.Emails
{
    public class ConsignmentShipfileEmailModel
    {
        public string ToCustomerName { get; set; }

        public string ToEmailAddress { get; set; }

        public ConsignmentShipfilePdfModel PdfAttachment { get; set; }
    }
}
