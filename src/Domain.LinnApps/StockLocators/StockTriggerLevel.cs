namespace Linn.Stores.Domain.LinnApps.StockLocators
{
    public class StockTriggerLevel
    {
        public int Id { get; set; }

        public string PartNumber { get; set; }

        public int? LocationId { get; set; }

        public decimal? TriggerLevel { get; set; }

        public decimal? MaxCapacity { get; set; }

        public int? PalletNumber { get; set; }

        public decimal? KanbanSize { get; set; }

        public StorageLocation StorageLocation { get; set; }
    }
}
