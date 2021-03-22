namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Resources.StockLocators;

    public class StockLocatorsWithStoragePlaceInfoResourceBuilder : IResourceBuilder<IEnumerable<StockLocatorWithStoragePlaceInfo>>
    {
    private readonly StockLocatorWithStoragePlaceInfoResourceBuilder stockLocatorResourceBuilder = new StockLocatorWithStoragePlaceInfoResourceBuilder();

    public IEnumerable<StockLocatorResource> Build(IEnumerable<StockLocatorWithStoragePlaceInfo> stockLocators)
    {
        return stockLocators.Select(a => this.stockLocatorResourceBuilder.Build(a));
    }

    object IResourceBuilder<IEnumerable<StockLocatorWithStoragePlaceInfo>>.Build(IEnumerable<StockLocatorWithStoragePlaceInfo> stockLocators) =>
        this.Build(stockLocators);

    public string GetLocation(IEnumerable<StockLocatorWithStoragePlaceInfo> stockLocators)
    {
        throw new NotImplementedException();
    }
    }
}
