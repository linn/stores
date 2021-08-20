namespace Linn.Stores.Resources.RequestResources
{
    public class StockMovesRequestResource : SearchRequestResource
    {
        public int? PalletNumber { get; set; }

        public int? LocationId { get; set; }
    }
}
