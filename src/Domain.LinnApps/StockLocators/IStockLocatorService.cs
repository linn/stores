namespace Linn.Stores.Domain.LinnApps.StockLocators
{
    using System.Collections.Generic;

    public interface IStockLocatorService
    {
        void UpdateStockLocator(StockLocator from, StockLocator to, IEnumerable<string> privileges);

        StockLocator CreateStockLocator(
            StockLocator toCreate, 
            string auditDepartmentCode, 
            IEnumerable<string> privileges);

        void DeleteStockLocator(StockLocator toDelete, IEnumerable<string> privileges);

        IEnumerable<StockLocatorWithStoragePlaceInfo> 
            GetStockLocatorsWithStoragePlaceInfoForPart(string partNumber);

        IEnumerable<StockLocator> GetBatches(string batches);

        IEnumerable<StockLocator> GetStockLocatorLocationsView(
            string partNumber,
            string location,
            string stockPool,
            string stockState,
            string batchRef);
    }
}
