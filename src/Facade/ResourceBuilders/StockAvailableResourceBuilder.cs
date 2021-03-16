namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockMove.Models;
    using Linn.Stores.Resources;

    public class StockAvailableResourceBuilder : IResourceBuilder<IEnumerable<StockAvailable>>
    {
        public IEnumerable<StockAvailableResource> Build(IEnumerable<StockAvailable> stockAvailable)
        {
            return stockAvailable.Select(
                a => new StockAvailableResource
                         {
                             LocationCode = a.LocationCode,
                             PalletNumber = a.PalletNumber,
                             LocationId = a.LocationId,
                             PartNumber = a.PartNumber,
                             QuantityAvailable = a.QuantityAvailable,
                             State = a.State,
                             StockPoolCode = a.StockPoolCode,
                             StockRotationDate = a.StockRotationDate.ToString("o")
                         });
        }

        object IResourceBuilder<IEnumerable<StockAvailable>>.Build(IEnumerable<StockAvailable> stockAvailable) => this.Build(stockAvailable);

        public string GetLocation(IEnumerable<StockAvailable> stockAvailable)
        {
            throw new NotImplementedException();
        }
    }
}
