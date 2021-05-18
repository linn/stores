namespace Linn.Stores.Domain.LinnApps.Models.Emails
{
    public class ConsignmentShipfileEmailModel
    {
        public string ToCustomerName { get; set; }

        public string ToEmailAddress { get; set; }

        public int ConsignmentNumber { get; set; }

        public string DateDispatched { get; set; }

        public string Carrier { get; set; }

        public string[] AddressLines { get; set; }

        public string Country { get; set; }

        public PackingListItem[] PackingList { get; set; }

        public DespatchNote[] DespatchNotes { get; set; }
    }
}
