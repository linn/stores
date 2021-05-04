namespace Linn.Stores.Domain.LinnApps
{
    public class ConsignmentShipfile
    {
        public int ConsignmentId { get; set; }

        public Consignment Consignment { get; set; }

        public string Message { get; set; }
    }
}
