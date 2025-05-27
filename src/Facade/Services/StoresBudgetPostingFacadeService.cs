namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class StoresBudgetPostingFacadeService : FacadeFilterService<StoresBudgetPosting, StoresBudgetPostingKey, StoresBudgetPostingResource, StoresBudgetPostingResource, StoresBudgetPostingResource>
    {
        public StoresBudgetPostingFacadeService(IRepository<StoresBudgetPosting, StoresBudgetPostingKey> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override StoresBudgetPosting CreateFromResource(StoresBudgetPostingResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(StoresBudgetPosting entity, StoresBudgetPostingResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<StoresBudgetPosting, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<StoresBudgetPosting, bool>> FilterExpression(StoresBudgetPostingResource searchResource)
        {
            return a => a.BudgetId == searchResource.BudgetId;
        }
    }
}
