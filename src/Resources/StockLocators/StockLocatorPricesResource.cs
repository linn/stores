namespace Linn.Stores.Resources.StockLocators
{
    public class StockLocatorPricesResource
    {
        public int? StockLocatorId { get; set; }

        public string PartNumber { get; set; }

        public int? QuantityAtLocation { get; set; }

        public int? QuantityAllocated { get; set; }

        public string Remarks { get; set; }

        public string State { get; set; }

        public string StockPool { get; set; }

        public int? Pallet { get; set; }

        public string LocationCode { get; set; }

        public string BatchRef { get; set; }

        public string BatchDate { get; set; }

        public int? BudgetId { get; set; }

        public decimal? PartPrice { get; set; }

        public decimal? MaterialPrice { get; set; }

        public decimal? LabourPrice { get; set; }

        public decimal? OverheadPrice { get; set; }
    }
}
