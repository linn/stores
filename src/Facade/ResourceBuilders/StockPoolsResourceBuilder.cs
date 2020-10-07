namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class StockPoolsResourceBuilder : IResourceBuilder<IEnumerable<StockPool>>
    {
        private readonly StockPoolResourceBuilder stockPoolResourceBuilder = new StockPoolResourceBuilder();

        public IEnumerable<StockPoolResource> Build(IEnumerable<StockPool> stockPools)
        {
            return stockPools
                .OrderBy(b => b.StockPoolCode)
                .Select(a => this.stockPoolResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<StockPool>>.Build(IEnumerable<StockPool> stockPools) => this.Build(stockPools);

        public string GetLocation(IEnumerable<StockPool> stockPools)
        {
            throw new NotImplementedException();
        }
    }
}