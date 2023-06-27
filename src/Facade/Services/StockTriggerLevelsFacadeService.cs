namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Logging;
    using Linn.Common.Persistence;
    using Linn.Common.Proxy.LinnApps;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    using Linn.Stores.Resources;

    public class StockTriggerLevelsFacadeService : FacadeService<StockTriggerLevel, int, StockTriggerLevelsResource, StockTriggerLevelsResource>, IStockTriggerLevelsFacadeService
    {
        private readonly IStockTriggerLevelsRepository repository;

        private readonly IRepository<StorageLocation, int> storageLocationRepository;

        private readonly ITransactionManager transactionManager;

        private readonly IDatabaseService databaseService;

        private readonly ILog logger;

        public StockTriggerLevelsFacadeService(
            IStockTriggerLevelsRepository repository,
            IRepository<StorageLocation, int> storageLocationRepository,
            ITransactionManager transactionManager,
            IDatabaseService databaseService,
            ILog logger) : base(repository, transactionManager)
        {
            this.repository = repository;
            this.storageLocationRepository = storageLocationRepository;
            this.transactionManager = transactionManager;
            this.databaseService = databaseService;
            this.logger = logger;
        }

        public IResult<StockTriggerLevel> DeleteStockTriggerLevel(int id, int userNumber)
        {
            var toDelete = this.repository.FindBy(s => s.Id == id);
            this.repository.Remove(toDelete);

            var message =
                $"StockTriggerLevel ID : {toDelete.Id}. "
                + $"LocationID : {toDelete.LocationId}. "
                + $"Part Number : {toDelete.PartNumber}. "
                + $"has been deleted by user : {userNumber}.";

            this.logger.Debug($"{message}");

            this.transactionManager.Commit();

            return new SuccessResult<StockTriggerLevel>(toDelete);
        }

        public IResult<IEnumerable<StockTriggerLevel>> SearchStockTriggerLevelsWithWildcard(
            string partNumberSearch,
            string storagePlaceSearch)
        {
            if (!string.IsNullOrEmpty(storagePlaceSearch) && storagePlaceSearch.ToUpper().StartsWith("P"))
            {
                storagePlaceSearch = storagePlaceSearch.Remove(0, 1);
            }
            
            return new SuccessResult<IEnumerable<StockTriggerLevel>>(
                this.repository.SearchStockTriggerLevelsWithWildCard(
                    partNumberSearch?.Trim().ToUpper(),
                    storagePlaceSearch?.Trim().ToUpper()));
        }

        protected override StockTriggerLevel CreateFromResource(StockTriggerLevelsResource resource)
        {
            var newStockTriggerLevel = new StockTriggerLevel 
                                           {
                                               Id = this.databaseService.GetIdSequence("STL_SEQ"),
                                               LocationId = resource.LocationId,
                                               TriggerLevel = resource.TriggerLevel,
                                               PartNumber = resource.PartNumber,
                                               PalletNumber = resource.PalletNumber,
                                               MaxCapacity = resource.MaxCapacity,
                                               KanbanSize = resource.KanbanSize,
                                               StorageLocation = resource.StorageLocation != null
                                                                     ? this.storageLocationRepository.FindBy(
                                                                         c => c.LocationCode == resource.StorageLocation.LocationCode)
                                                                     : null
                                           };

            return newStockTriggerLevel;
        }

        protected override void UpdateFromResource(
            StockTriggerLevel entity,
            StockTriggerLevelsResource updateResource)
        {
            entity.LocationId = updateResource.LocationId;
            entity.TriggerLevel = updateResource.TriggerLevel;
            entity.PartNumber = updateResource.PartNumber;
            entity.PalletNumber = updateResource.PalletNumber;
            entity.MaxCapacity = updateResource.MaxCapacity;
            entity.KanbanSize = updateResource.KanbanSize;
            entity.StorageLocation = updateResource.StorageLocation != null
                                ? this.storageLocationRepository.FindBy(c => c.LocationCode == updateResource.StorageLocation.LocationCode)
                                : null;
        }

        protected override Expression<Func<StockTriggerLevel, bool>> SearchExpression(string searchTerm)
        {
            return imps => imps.PalletNumber.ToString().ToUpper().Contains(searchTerm) ||
                           imps.PartNumber.ToUpper().Contains(searchTerm);
        }
    }
}
