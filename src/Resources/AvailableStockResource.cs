namespace Linn.Stores.Resources
{
    public class AvailableStockResource
    {
        public string PartNumber { get; set; }

        public decimal QuantityAvailable { get; set; }

        public string StockRotationDate { get; set; }

        public int? LocationId { get; set; }

        public string LocationCode { get; set; }

        public int? PalletNumber { get; set; }

        public string StockPoolCode { get; set; }

        public string State { get; set; }

        public string DisplayLocation { get; set; }

        public string DisplayMoveLocation { get; set; }
    }
}
