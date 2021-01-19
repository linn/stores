namespace Linn.Stores.Resources
{
    public class StockLocatorResource
    {
        public string PartNumber { get; set; }

        public int? LocationId { get; set; }

        public string BatchRef { get; set; }

        public string BatchDate { get; set; }

        public int? Quantity { get; set; }

        public string Remarks { get; set; }
    }
}