namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class StockLocatorResourceBuilder : IResourceBuilder<StockLocator>
    {
        public StockLocatorResource Build(StockLocator stockLocator)
        {
            if (stockLocator == null) return null;
            return new StockLocatorResource
                       {
                           Id = stockLocator.Id,
                           BatchRef = stockLocator.BatchRef,
                           Remarks = stockLocator.Remarks,
                           BatchDate = stockLocator.StockRotationDate?.ToString("o"),
                           Quantity = stockLocator.Quantity,
                           LocationId = stockLocator.LocationId
                       };
        }

        public string GetLocation(StockLocator stockLocator)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<StockLocator>.Build(StockLocator stockLocator) => this.Build(stockLocator);
    }
}
