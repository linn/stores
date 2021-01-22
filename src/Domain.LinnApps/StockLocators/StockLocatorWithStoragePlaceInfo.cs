namespace Linn.Stores.Domain.LinnApps.StockLocators
{
    public class StockLocatorWithStoragePlaceInfo : StockLocator
    {
        public string StoragePlaceName { get; set; }

        public string StoragePlaceDescription { get; set; }
    }
}
