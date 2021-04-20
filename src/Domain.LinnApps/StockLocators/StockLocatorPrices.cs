namespace Linn.Stores.Domain.LinnApps.StockLocators
{
    using System;

    public class StockLocatorPrices
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

        public DateTime? BatchDate { get; set; }

        public int? BudgetId { get; set; }

        public decimal? PartPrice { get; set; }

        public decimal? MaterialPrice { get; set; }

        public decimal? LabourPrice { get; set; }

        public decimal? OverheadPrice { get; set; }

        public string Category { get; set; }
    }
}
