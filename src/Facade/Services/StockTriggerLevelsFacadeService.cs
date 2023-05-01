namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;

    using Linn.Stores.Domain.LinnApps.StockLocators;

    using Linn.Stores.Resources;

    public class StockTriggerLevelsFacadeService : FacadeService<StockTriggerLevel, int, StockTriggerLevelsResource, StockTriggerLevelsResource>, IStockTriggerLevelsFacadeService
    {
        private readonly IRepository<StockTriggerLevel, int> repository;

        private readonly ITransactionManager transactionManager;

        public StockTriggerLevelsFacadeService(IRepository<StockTriggerLevel, int> repository, ITransactionManager transactionManager) : base(repository, transactionManager)
        {
            this.repository = repository;
            this.transactionManager = transactionManager;
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
