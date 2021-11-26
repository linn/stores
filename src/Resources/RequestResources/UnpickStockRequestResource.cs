namespace Linn.Stores.Resources.RequestResources
{
    public class UnpickStockRequestResource
    {
        public int ReqNumber { get; set; }

        public int LineNumber { get; set; }

        public int OrderNumber { get; set; }

        public int OrderLine { get; set; }

        public int AmendedBy { get; set; }

        public int? PalletNumber { get; set; }

        public int? LocationId { get; set; }
    }
}
