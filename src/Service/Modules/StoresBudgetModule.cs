namespace Linn.Stores.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;
    using Linn.Stores.Service.Models;

    using Nancy;

    public sealed class StoresBudgetModule : NancyModule
    {
        private readonly IFacadeFilterService<StoresBudgetPosting, StoresBudgetPostingKey, StoresBudgetPostingResource, StoresBudgetPostingResource, StoresBudgetPostingResource> storesBudgetPostingFacadeFilterService;

        public StoresBudgetModule(IFacadeFilterService<StoresBudgetPosting, StoresBudgetPostingKey, StoresBudgetPostingResource, StoresBudgetPostingResource, StoresBudgetPostingResource> storesBudgetPostingFacadeFilterService)
        {
            this.storesBudgetPostingFacadeFilterService = storesBudgetPostingFacadeFilterService;

            this.Get("/inventory/stores-budget-postings/{budgetId}", parameters => this.GetStoresBudgetPostingsByBudgetId(parameters.budgetId));
        }

        private object GetStoresBudgetPostingsByBudgetId(int budgetId)
        {
            var resource = new StoresBudgetPostingResource { BudgetId = budgetId };
            var results = this.storesBudgetPostingFacadeFilterService.FilterBy(resource);

            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get())
                .WithView("Index");
        }
    }
}
