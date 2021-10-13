namespace Linn.Stores.Domain.LinnApps.StockLocators
{
    using System;
    using System.Collections.Generic;

    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Domain.LinnApps.Requisitions;

    public class StockLocator
    {
        public int Id { get; set; }

        public int? Quantity { get; set; }

        public int? PalletNumber { get; set; }

        public int? LocationId { get; set; }

        public StorageLocation StorageLocation { get; set; }

        public int? BudgetId { get; set; }

        public string PartNumber { get; set; }

        public Part Part { get; set; }

        public int? QuantityAllocated { get; set; }

        public string StockPoolCode { get; set; }

        public string Remarks { get; set; }

        public DateTime? StockRotationDate { get; set; }

        public string BatchRef { get; set; }

        public string State { get; set; }

        public string Category { get; set; }

        public IEnumerable<ReqMove> ReqMoves { get; set; }

        public StockTriggerLevel TriggerLevel { get; set; }
    }
}
