namespace Linn.Stores.Domain.LinnApps
{
    using System.Collections.Generic;

    using Linn.Stores.Domain.LinnApps.StockLocators;

    public interface IStockLocatorService
    {
        void UpdateStockLocator(StockLocator from, StockLocator to);

        StockLocator CreateStockLocator(StockLocator toCreate, string auditDepartmentCode);

        void DeleteStockLocator(StockLocator toDelete);

        IEnumerable<StockLocatorWithStoragePlaceInfo> 
            GetStockLocatorsWithStoragePlaceInfoForPart(string partNumber);
    }
}
