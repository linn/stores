namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Runtime.InteropServices.ComTypes;

    using Linn.Common.Facade;
    using Linn.Common.Logging;
    using Linn.Common.Persistence;
    using Linn.Common.Proxy.LinnApps;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    using Linn.Stores.Resources;

    public class StockTriggerLevelsFacadeService : FacadeService<StockTriggerLevel, int, StockTriggerLevelsResource, StockTriggerLevelsResource>, IStockTriggerLevelsFacadeService
    {
        private readonly IRepository<StockTriggerLevel, int> repository;

        private readonly ITransactionManager transactionManager;

        private readonly IDatabaseService databaseService;

        private readonly ILog logger;

        public StockTriggerLevelsFacadeService(
            IRepository<StockTriggerLevel, int> repository, 
            ITransactionManager transactionManager,
            IDatabaseService databaseService,
            ILog logger) : base(repository, transactionManager)
        {
            this.repository = repository;
            this.transactionManager = transactionManager;
            this.databaseService = databaseService;
            this.logger = logger;
        }

        public IResult<StockTriggerLevel> DeleteStockTriggerLevel(int id)
        {
            var toDelete = this.repository.FindBy(s => s.Id == id);
            this.repository.Remove(toDelete);

            return new SuccessResult<StockTriggerLevel>(toDelete);
        }

        protected override StockTriggerLevel CreateFromResource(StockTriggerLevelsResource resource)
        {
            var newStockTriggerLevel = new StockTriggerLevel 
                                           {
                                               Id = this.databaseService.GetNextVal("STL_ID"),
                                               LocationId = resource.LocationId,
                                               TriggerLevel = resource.TriggerLevel,
                                               PartNumber = resource.PartNumber,
                                               PalletNumber = resource.PalletNumber,
                                               MaxCapacity = resource.MaxCapacity,
                                               KanbanSize = resource.KanbanSize
                                           };

            return newStockTriggerLevel;
        }

        protected override void UpdateFromResource(
            StockTriggerLevel entity,
            StockTriggerLevelsResource updateResource)
        {
            entity.Id = updateResource.Id;
            entity.LocationId = updateResource.LocationId;
            entity.TriggerLevel = updateResource.TriggerLevel;
            entity.PartNumber = updateResource.PartNumber;
            entity.PalletNumber = updateResource.PalletNumber;
            entity.MaxCapacity = updateResource.MaxCapacity;
            entity.KanbanSize = updateResource.KanbanSize;
        }

        protected override Expression<Func<StockTriggerLevel, bool>> SearchExpression(string searchTerm)
        {
            return imps => imps.LocationId.ToString().Contains(searchTerm);
        }
    }
}
