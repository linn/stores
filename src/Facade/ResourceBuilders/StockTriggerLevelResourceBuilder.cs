namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Resources;

    public class StockTriggerLevelResourceBuilder : IResourceBuilder<StockTriggerLevel>
    {
        private readonly StorageLocationResourceBuilder storageLocationResourceBuilder = new StorageLocationResourceBuilder();

        public StockTriggerLevelsResource Build(StockTriggerLevel stockTriggerLevel)
        {
            return new StockTriggerLevelsResource
            { 
                Id = stockTriggerLevel.Id,
                PartNumber = stockTriggerLevel.PartNumber,
                LocationId = stockTriggerLevel.LocationId,
                TriggerLevel = stockTriggerLevel.TriggerLevel,
                MaxCapacity = stockTriggerLevel.MaxCapacity,
                PalletNumber = stockTriggerLevel.PalletNumber,
                KanbanSize = stockTriggerLevel.KanbanSize,
                StorageLocation =
                stockTriggerLevel.StorageLocation != null
                                   ? this.storageLocationResourceBuilder.Build(stockTriggerLevel.StorageLocation)
                                   : null,
                Links = this.BuildLinks(stockTriggerLevel).ToArray()
            };
        }

        public string GetLocation(StockTriggerLevel stockTriggerLevel)
        {
            return $"/inventory/stock-trigger-levels/{stockTriggerLevel.Id}";
        }

        object IResourceBuilder<StockTriggerLevel>.Build(StockTriggerLevel stockTriggerLevel) => this.Build(stockTriggerLevel);

        private IEnumerable<LinkResource> BuildLinks(StockTriggerLevel stockTriggerLevel)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(stockTriggerLevel) };
        }
    }
}
