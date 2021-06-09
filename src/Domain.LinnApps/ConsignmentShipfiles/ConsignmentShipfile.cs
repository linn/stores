namespace Linn.Stores.Domain.LinnApps.ConsignmentShipfiles
{
    using Linn.Stores.Domain.LinnApps.Consignments;

    public class ConsignmentShipfile
    {
        public int Id { get; set; }

        public int ConsignmentId { get; set; }

        public Consignment Consignment { get; set; }

        public string Message { get; set; }

        public string ShipfileSent { get; set; }
    }
}
