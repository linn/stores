namespace Linn.Stores.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.RequestResources;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class StockLocatorsModule : NancyModule
    {
        private readonly IFacadeFilterService<StockLocator, int, StockLocatorResource, StockLocatorResource, SearchRequestResource> service;

        public StockLocatorsModule(
            IFacadeFilterService<StockLocator, int, StockLocatorResource, StockLocatorResource, SearchRequestResource> service)
        {
            this.service = service;
            this.Get("/inventory/stock-locators", _ => this.GetStockLocators());
        }

        private object GetStockLocators()
        {
            var resource = this.Bind<SearchRequestResource>();
            return this.Negotiate.WithModel(this.service.FilterBy(resource));
        }
    }
}
