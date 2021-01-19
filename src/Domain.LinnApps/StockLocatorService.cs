namespace Linn.Stores.Domain.LinnApps
{
    using Linn.Common.Persistence;

    public class StockLocatorService : IStockLocatorService
    {
        private readonly IRepository<StockLocator, int> repository;

        public StockLocatorService(IRepository<StockLocator, int> repository)
        {
            this.repository = repository;
        }

        public void UpdateStockLocator(StockLocator @from, StockLocator to)
        {
            throw new System.NotImplementedException();
        }

        public StockLocator CreateStockLocator(StockLocator partToCreate)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteStockLocator(StockLocator toDelete)
        {
            this.repository.Remove(toDelete);
        }
    }
}
