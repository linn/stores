namespace Linn.Stores.Domain.LinnApps
{
    using System.Collections.Generic;

    using Linn.Stores.Domain.LinnApps.StockLocators;

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
    }
}
