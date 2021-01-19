namespace Linn.Stores.Service.Modules
{
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class StockLocatorsModule : NancyModule
    {
        private readonly IStockLocatorFacadeService service;

        public StockLocatorsModule(
            IStockLocatorFacadeService service)
        {
            this.service = service;
            this.Get("/inventory/stock-locators", _ => this.GetStockLocators());
            this.Delete("/inventory/stock-locators", _ => this.DeleteStockLocator());
        }

        private object GetStockLocators()
        {
            var resource = this.Bind<StockLocatorResource>();
            return this.Negotiate.WithModel(this.service.FilterBy(resource));
        }

        private object DeleteStockLocator()
        {
            var resource = this.Bind<StockLocatorResource>();
            return this.Negotiate.WithModel(this.service.Delete(resource.Id));
        }
    }
}
