namespace Linn.Stores.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class StockLocatorsModule : NancyModule
    {
        private readonly IFacadeFilterService<StockLocator, int, StockLocatorResource, StockLocatorResource, StockLocatorResource> service;

        public StockLocatorsModule(
            IFacadeFilterService<StockLocator, int, StockLocatorResource, StockLocatorResource, StockLocatorResource> service)
        {
            this.service = service;
            this.Get("/inventory/stock-locators", _ => this.GetStockLocators());
        }

        private object GetStockLocators()
        {
            var resource = this.Bind<StockLocatorResource>();
            return this.Negotiate.WithModel(this.service.FilterBy(resource));
        }
    }
}
