namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Resources.StockLocators;

    public class StockQuantitiesListResourceBuilder : IResourceBuilder<IEnumerable<StockQuantities>>
    {
        private readonly StockQuantitiesResourceBuilder resourceBuilder
            = new StockQuantitiesResourceBuilder();

        public IEnumerable<StockQuantitiesResource> Build(IEnumerable<StockQuantities> stockLocators)
        {
            return stockLocators
                .Select(a => this.resourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<StockQuantities>>.Build(IEnumerable<StockQuantities> stockLocators)
            => this.Build(stockLocators);

        public string GetLocation(IEnumerable<StockQuantities> stockLocators)
        {
            throw new NotImplementedException();
        }
    }
}
