namespace Linn.Stores.Domain.LinnApps.Consignments
{
    public class ConsignmentPallet
    {
        public int ConsignmentId { get; set; }

        public int PalletNumber { get; set; }

        public int? Weight { get; set; }

        public int? Height { get; set; }

        public int? Width { get; set; }

        public int? Depth { get; set; }
    }
}
