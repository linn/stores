namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class StockLocatorsFacadeService : 
        FacadeFilterService<StockLocator, int, StockLocatorResource, StockLocatorResource, StockLocatorResource>
    {
        public StockLocatorsFacadeService(IRepository<StockLocator, int> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override StockLocator CreateFromResource(StockLocatorResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(StockLocator entity, StockLocatorResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<StockLocator, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<StockLocator, bool>> FilterExpression(StockLocatorResource filterResource)
        {
            return x => x.PartNumber == filterResource.PartNumber;
        }
    }
}
