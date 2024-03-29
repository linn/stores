﻿namespace Linn.Stores.Domain.LinnApps.StockLocators
{
    using Linn.Stores.Domain.LinnApps.Parts;

    public class StockLocatorLocation
    {
        public decimal Quantity { get; set; }

        public int StorageLocationId { get; set; }

        public StorageLocation StorageLocation { get; set; }

        public string PartNumber { get; set; }

        public Part Part { get; set; }

        public string PartDescription { get; set; }

        public int? PalletNumber { get; set; }

        public string LocationType { get; set; }

        public string State { get; set; }

        public string Category { get; set; }

        public string StockPoolCode { get; set; }

        public string OurUnitOfMeasure { get; set; }

        public decimal? QuantityAllocated { get; set; }
    }
}
