namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Resources.StockLocators;

    public class StockLocatorPricesListResourceBuilder : IResourceBuilder<IEnumerable<StockLocatorPrices>>
    {
        private readonly StockLocatorPricesResourceBuilder stockLocatorPricesResourceBuilder
            = new StockLocatorPricesResourceBuilder();

        public IEnumerable<StockLocatorPricesResource> Build(IEnumerable<StockLocatorPrices> stockLocatorsPricesList)
        {
            return stockLocatorsPricesList
                .Select(a => this.stockLocatorPricesResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<StockLocatorPrices>>.Build(IEnumerable<StockLocatorPrices> stockLocatorPrices)
            => this.Build(stockLocatorPrices);

        public string GetLocation(IEnumerable<StockLocatorPrices> stockLocators)
        {
            throw new NotImplementedException();
        }
    }
}
