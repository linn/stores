namespace Linn.Stores.Domain.LinnApps
{
    public interface IStockLocatorService
    {
        void UpdateStockLocator(StockLocator from, StockLocator to);

        StockLocator CreateStockLocator(StockLocator toCreate, string auditDepartmentCode);

        void DeleteStockLocator(StockLocator toDelete);
    }
}
