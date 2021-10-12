namespace Linn.Stores.Domain.LinnApps.StockLocators
{
    public class StockTriggerLevel
    {
        public string PartNumber { get; set; }

        public int LocationId { get; set; }

        public decimal? TriggerLevel { get; set; }

        public decimal? MaxCapacity { get; set; }
    }
}
