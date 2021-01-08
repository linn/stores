namespace Linn.Stores.Domain.LinnApps
{
    public class StockLocator
    {
        public int? Quantity { get; set; }

        public int? PalletNumber { get; set; }

        public int? LocationId { get; set; }

        public int? BudgetId { get; set; }

        public string PartNumber { get; set; }

        public int? QuantityAllocated { get; set; }
    }
}