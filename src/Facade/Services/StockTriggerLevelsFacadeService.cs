namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Common.Proxy.LinnApps;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Proxy;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.StockLocators;

    public class StockTriggerLevelsFacadeService : FacadeService<StockTriggerLevel, int, StockTriggerLevelsResource, StockTriggerLevelsResource>
    {
        public StockTriggerLevelsFacadeService(IRepository<StockTriggerLevel, int> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
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
