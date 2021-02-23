namespace Linn.Stores.Service.Modules
{
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class SalesAccountModule : NancyModule
    {
        private readonly ISalesAccountService salesAccountService;

        public SalesAccountModule(ISalesAccountService salesAccountService)
        {
            this.salesAccountService = salesAccountService;
            this.Get("/inventory/sales-accounts", parameters => this.GetSalesAccounts());
        }

        private object GetSalesAccounts()
        {
            var resource = this.Bind<SearchRequestResource>();

            var results = this.salesAccountService.SearchSalesAccounts(resource.SearchTerm);

            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get())
                .WithView("Index");
        }
    }
}