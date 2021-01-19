namespace Linn.Stores.Domain.LinnApps
{
    public interface IStockLocatorService
    {
        void UpdateStockLocator(StockLocator from, StockLocator to);

        StockLocator CreateStockLocator(StockLocator toCreate);

        void DeleteStockLocator(StockLocator toDelete);
    }
}
