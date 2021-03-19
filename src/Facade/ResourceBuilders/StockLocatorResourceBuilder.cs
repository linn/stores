namespace Linn.Stores.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Resources;

    public class StockLocatorResourceBuilder : IResourceBuilder<StockLocator>
    {
        public StockLocatorResource Build(StockLocator stockLocator)
        {
            return new StockLocatorResource
                       {
                           Id = stockLocator.Id,
                           BatchRef = stockLocator.BatchRef,
                           Remarks = stockLocator.Remarks,
                           StockRotationDate = stockLocator.StockRotationDate?.ToString("o"),
                           Quantity = stockLocator.Quantity,
                           QuantityAllocated = stockLocator.QuantityAllocated,
                           LocationId = stockLocator.LocationId,
                           PalletNumber = stockLocator.PalletNumber,
                           PartNumber = stockLocator.PartNumber,
                           PartDescription = stockLocator.Part?.Description,
                           PartUnitOfMeasure = stockLocator.Part?.OurUnitOfMeasure,
                           LocationName = stockLocator.StorageLocation?.LocationCode,
                           StockPoolCode = stockLocator.StockPoolCode,
                           LocationDescription = stockLocator.StorageLocation?.Description,
                           State = stockLocator.State,
                           PartId = stockLocator.Part?.Id,
                           Category = stockLocator.Category
                       };
        }

        public string GetLocation(StockLocator stockLocator)
        {
           return $"inventory/stock-locators/{stockLocator.Id}";
        }

        object IResourceBuilder<StockLocator>.Build(StockLocator stockLocator) => this.Build(stockLocator);
    }
}
