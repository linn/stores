namespace Linn.Stores.Service.Modules
{
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class SalesOutletModule : NancyModule
    {
        private readonly ISalesOutletService salesOutletService;

        public SalesOutletModule(ISalesOutletService salesOutletService)
        {
            this.salesOutletService = salesOutletService;
            this.Get("/inventory/sales-outlets", parameters => this.GetSalesOutlets());
        }

        private object GetSalesOutlets()
        {
            var resource = this.Bind<SearchRequestResource>();

            var results = this.salesOutletService.GetSalesOutlets(resource.SearchTerm);

            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get())
                .WithView("Index");
        }
    }
}