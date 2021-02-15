namespace Linn.Stores.Domain.LinnApps.StockLocators
{
    public class StockLocatorLocationsViewModel
    {
        public int Quantity { get; set; }

        public int StorageLocationId { get; set; }

        public StorageLocation StorageLocation { get; set; }

        public string PartNumber { get; set; }

        public int? PalletNumber { get; set; }

        public string LocationType { get; set; }

        public string State { get; set; }

        public string Category { get; set; }

        public string StockPoolCode { get; set; }

        public string OurUnitOfMeasure { get; set; }

        public string BatchRef { get; set; }

        public int? QuantityAllocated { get; set; }
    }
}
