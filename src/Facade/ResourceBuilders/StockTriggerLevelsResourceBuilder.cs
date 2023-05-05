namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.StockLocators;

    public class StockTriggerLevelsResourceBuilder : IResourceBuilder<IEnumerable<StockTriggerLevel>>
    {
        private readonly StockTriggerLevelResourceBuilder stockTriggerLevelResourceBuilder = new StockTriggerLevelResourceBuilder();

        public IEnumerable<StockTriggerLevelsResource> Build(IEnumerable<StockTriggerLevel> stockTriggerLevels)
        {
            return stockTriggerLevels.Select(s => this.stockTriggerLevelResourceBuilder.Build(s));
        } 

        object IResourceBuilder<IEnumerable<StockTriggerLevel>>.Build(IEnumerable<StockTriggerLevel> stockTriggerLevels) =>
            this.Build(stockTriggerLevels);

        public string GetLocation(IEnumerable<StockTriggerLevel> stockTriggerLevels)
        {
            throw new NotImplementedException();
        }
    }
}
