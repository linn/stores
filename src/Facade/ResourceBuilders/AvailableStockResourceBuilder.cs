namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockMove.Models;
    using Linn.Stores.Resources;

    public class AvailableStockResourceBuilder : IResourceBuilder<IEnumerable<AvailableStock>>
    {
        public IEnumerable<AvailableStockResource> Build(IEnumerable<AvailableStock> availableStock)
        {
            return availableStock.Select(
                a => new AvailableStockResource
                         {
                             LocationCode = a.LocationCode,
                             PalletNumber = a.PalletNumber,
                             LocationId = a.LocationId,
                             PartNumber = a.PartNumber,
                             QuantityAvailable = a.QuantityAvailable,
                             State = a.State,
                             StockPoolCode = a.StockPoolCode,
                             StockRotationDate = a.StockRotationDate.ToString("o"),
                             DisplayLocation = a.DisplayLocation,
                             DisplayMoveLocation = a.DisplayMoveLocation
                         });
        }

        object IResourceBuilder<IEnumerable<AvailableStock>>.Build(IEnumerable<AvailableStock> availableStock) => this.Build(availableStock);

        public string GetLocation(IEnumerable<AvailableStock> availableStock)
        {
            throw new NotImplementedException();
        }
    }
}
