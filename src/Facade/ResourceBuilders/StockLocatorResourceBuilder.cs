namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class StockLocatorResourceBuilder : IResourceBuilder<StockLocator>
    {
        public StockLocatorResource Build(StockLocator stockLocator)
        {
            return new StockLocatorResource
                       {
                           BatchRef = stockLocator.BatchRef,
                           Remarks = stockLocator.Remarks,
                           BatchDate = stockLocator.StockRotationDate?.ToString("o"),
                           Quantity = stockLocator.Quantity,
                           //StoragePlaceDescription = stockLocator.LocationId, need to work out how to get these?
                           //StoragePlaceName = stockLocator.PalletNumber
                       };
        }

        public string GetLocation(StockLocator stockLocator)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<StockLocator>.Build(StockLocator stockLocator) => this.Build(stockLocator);

        private IEnumerable<LinkResource> BuildLinks(StockLocator stockLocator)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(stockLocator) };
        }
    }
}
