namespace Linn.Stores.Resources.RequestResources
{
    public class StockMovesRequestResource
    {
        public string PartNumber { get; set; }

        public int? PalletNumber { get; set; }

        public int? LocationId { get; set; }
    }
}
