namespace Linn.Stores.Domain.LinnApps
{
    using System.Linq;

    using Linn.Common.Persistence;

    public class StockLocatorService : IStockLocatorService
    {
        private readonly IRepository<StockLocator, int> repository;

        private readonly IRepository<StoresPallet, int> palletRepository;

        public StockLocatorService(
            IRepository<StockLocator, int> repository,
            IRepository<StoresPallet, int> palletRepository)
        {
            this.repository = repository;
            this.palletRepository = palletRepository;
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
            if (!this.repository
                    .FilterBy(l => l.PalletNumber == toDelete.PalletNumber && l.Quantity > 0).Any())
            {
                return;
            }

            foreach (var storesPallet in 
                this.palletRepository.FilterBy(p => p.PalletNumber == toDelete.PalletNumber))
            {
                storesPallet.AuditFrequencyWeeks = null;
                storesPallet.AuditedByDepartmentCode = null;
            }
        }
    }
}
