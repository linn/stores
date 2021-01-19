namespace Linn.Stores.Domain.LinnApps
{
    using System;

    public class StockLocator
    {
        public int Id { get; set; }

        public int? Quantity { get; set; }

        public int? PalletNumber { get; set; }

        public int? LocationId { get; set; }

        public int? BudgetId { get; set; }

        public string PartNumber { get; set; }

        public int? QuantityAllocated { get; set; }

        public string StockPoolCode { get; set; }

        public string Remarks { get; set; }

        public DateTime? StockRotationDate { get; set; }

        public string BatchRef { get; set; }
    }
}
