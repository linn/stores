namespace Linn.Stores.Domain.LinnApps.Models.Emails
{
    public class ConsignmentShipfilePdfModel
    {
        public string ConsignmentNumber { get; set; }

        public string DateDispatched { get; set; }

        public string Carrier { get; set; }

        public string[] Address { get; set; }

        public string Reference { get; set; }

        public string CarriersReference { get; set; }

        public string[] OutletAddress { get; set; }

        public PackingListItem[] PackingList { get; set; }

        public DespatchNote[] DespatchNotes { get; set; }
    }
}
