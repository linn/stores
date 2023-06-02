namespace Linn.Stores.Resources
{
    using Linn.Common.Resources;
    using Linn.Stores.Resources.StockLocators;

    public class StockTriggerLevelsResource : HypermediaResource
    {
        public int Id { get; set; }

        public string PartNumber { get; set; }

        public int? LocationId { get; set; }

        public decimal? TriggerLevel { get; set; }

        public decimal? MaxCapacity { get; set; }

        public int? PalletNumber { get; set; }

        public decimal? KanbanSize { get; set; }

        public StorageLocationResource StorageLocation { get; set; }
    }
}
