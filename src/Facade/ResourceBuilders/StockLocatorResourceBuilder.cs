namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Proxy;
    using Linn.Stores.Resources.StockLocators;

    public class StockLocatorResourceBuilder : IResourceBuilder<StockLocator>
    {
        private readonly IProductsService productsService;

        public StockLocatorResourceBuilder(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public StockLocatorResource Build(StockLocator stockLocator)
        {
            return new StockLocatorResource
                       {
                           Id = stockLocator.Id,
                           BatchRef = stockLocator.BatchRef,
                           Remarks = stockLocator.Remarks,
                           StockRotationDate = stockLocator.StockRotationDate?.ToString("o"),
                           Quantity = stockLocator.Quantity,
                           QuantityAllocated = stockLocator.QuantityAllocated,
                           LocationId = stockLocator.LocationId,
                           PalletNumber = stockLocator.PalletNumber,
                           PartNumber = stockLocator.PartNumber,
                           PartDescription = stockLocator.Part?.Description,
                           PartUnitOfMeasure = stockLocator.Part?.OurUnitOfMeasure,
                           LocationName = stockLocator.StorageLocation?.LocationCode,
                           StockPoolCode = stockLocator.StockPoolCode,
                           LocationDescription = stockLocator.StorageLocation?.Description,
                           State = stockLocator.State,
                           PartId = stockLocator.Part?.Id,
                           Category = stockLocator.Category,
                           TriggerLevel = stockLocator.TriggerLevel?.TriggerLevel,
                           MaxCapacity = stockLocator.TriggerLevel?.MaxCapacity,
                           Links = this.BuildLinks(stockLocator).ToArray(),
                       };
        }

        public string GetLocation(StockLocator stockLocator)
        {
           return $"inventory/stock-locators/{stockLocator.Id}";
        }

        object IResourceBuilder<StockLocator>.Build(StockLocator stockLocator) => this.Build(stockLocator);

        private IEnumerable<LinkResource> BuildLinks(StockLocator stockLocator)
        {
            if (stockLocator.Part != null)
            {
                yield return new LinkResource { Rel = "part", Href = $"/parts/{stockLocator.Part.Id}" };
                yield return new LinkResource { Rel = "product", Href = this.productsService.GetLinkToProduct(stockLocator.PartNumber) };
                yield return new LinkResource
                                 {
                                     Rel = "part-used-on",
                                     Href = $"/purchasing/material-requirements/used-on-report?partNumber={stockLocator.PartNumber}"
                                 };
            }
        }
    }
}
