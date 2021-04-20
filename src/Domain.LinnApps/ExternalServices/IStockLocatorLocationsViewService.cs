namespace Linn.Stores.Domain.LinnApps.ExternalServices
{
    using System.Collections.Generic;

    using Linn.Stores.Domain.LinnApps.StockLocators;

    public interface IStockLocatorLocationsViewService
    {
        IEnumerable<StockLocatorLocation> QueryView(
            string partNumber,
            int? locationId,
            int? palletNumber,
            string stockPool,
            string stockState,
            string category);
    }
}
