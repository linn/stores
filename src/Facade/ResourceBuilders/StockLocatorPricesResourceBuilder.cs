namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Resources.StockLocators;

    public class StockLocatorPricesResourceBuilder : IResourceBuilder<StockLocatorPrices>
    {
        public StockLocatorPricesResource Build(StockLocatorPrices stockLocatorPrices)
        {
            return new StockLocatorPricesResource
                       {
                           StockLocatorId = stockLocatorPrices.StockLocatorId,
                           BatchRef = stockLocatorPrices.BatchRef,
                           Remarks = stockLocatorPrices.Remarks,
                           BatchDate = stockLocatorPrices.BatchDate?.ToString("o"),
                           QuantityAtLocation = stockLocatorPrices.QuantityAtLocation,
                           LocationCode = stockLocatorPrices.LocationCode,
                           Pallet = stockLocatorPrices.Pallet,
                           PartNumber = stockLocatorPrices.PartNumber,
                           StockPool = stockLocatorPrices.StockPool,
                           State = stockLocatorPrices.State,
                           BudgetId = stockLocatorPrices.BudgetId,
                           MaterialPrice = stockLocatorPrices.MaterialPrice,
                           LabourPrice = stockLocatorPrices.LabourPrice,
                           OverheadPrice = stockLocatorPrices.OverheadPrice,
                           PartPrice = stockLocatorPrices.PartPrice
                       };
        }

        public string GetLocation(StockLocatorPrices stockLocator)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<StockLocatorPrices>.Build(StockLocatorPrices stockLocatorPrices) => this.Build(stockLocatorPrices);
    }
}
