namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Resources;

    public class StockLocatorWithStoragePlaceInfoResourceBuilder : IResourceBuilder<StockLocatorWithStoragePlaceInfo>
    {
        public StockLocatorResource Build(StockLocatorWithStoragePlaceInfo stockLocator)
        {
            return new StockLocatorResource
                       {
                           Id = stockLocator.Id,
                           BatchRef = stockLocator.BatchRef,
                           Remarks = stockLocator.Remarks,
                           StockRotationDate = stockLocator.StockRotationDate?.ToString("o"),
                           Quantity = stockLocator.Quantity,
                           LocationId = stockLocator.LocationId,
                           PalletNumber = stockLocator.PalletNumber,
                           StoragePlaceName = stockLocator.StoragePlaceName,
                           StoragePlaceDescription = stockLocator.StoragePlaceDescription,
                           PartNumber = stockLocator.PartNumber,
                           AuditDepartmentCode = stockLocator.AuditDepartmentCode
                       };
        }

        public string GetLocation(StockLocatorWithStoragePlaceInfo stockLocator)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<StockLocatorWithStoragePlaceInfo>.Build(StockLocatorWithStoragePlaceInfo stockLocator) => this.Build(stockLocator);
    }
}
