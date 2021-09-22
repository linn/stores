namespace Linn.Stores.Resources.Consignments
{
    public class ConsignmentPalletResource
    {
        public int ConsignmentId { get; set; }

        public int PalletNumber { get; set; }

        public int? Weight { get; set; }

        public int? Height { get; set; }

        public int? Width { get; set; }

        public int? Depth { get; set; }
    }
}
