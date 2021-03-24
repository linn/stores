namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Resources.StockLocators;

    public class StockLocatorsResourceBuilder : IResourceBuilder<IEnumerable<StockLocator>>
    {
        private readonly StockLocatorResourceBuilder stockLocatorResourceBuilder 
            = new StockLocatorResourceBuilder();

        public IEnumerable<StockLocatorResource> Build(IEnumerable<StockLocator> stockLocators)
        {
            return stockLocators
                .Select(a => this.stockLocatorResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<StockLocator>>.Build(IEnumerable<StockLocator> stockLocators) 
            => this.Build(stockLocators);

        public string GetLocation(IEnumerable<StockLocator> stockLocators)
        {
            throw new NotImplementedException();
        }
    }
}
