namespace Linn.Stores.Resources
{
    public class StockAvailableResource
    {
        public string PartNumber { get; set; }

        public int QuantityAvailable { get; set; }

        public string StockRotationDate { get; set; }

        public int? LocationId { get; set; }

        public string LocationCode { get; set; }

        public int? PalletNumber { get; set; }

        public string StockPoolCode { get; set; }

        public string State { get; set; }
    }
}
