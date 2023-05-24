namespace Linn.Stores.Resources.RequestResources
{
    public class StockTriggerLevelsSearchRequestResource : SearchRequestResource
    {
        public string PartNumberSearchTerm { get; set; }

        public string StoragePlaceSearchTerm { get; set; }
    }
}
