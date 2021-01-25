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
            this.Put("/inventory/stock-locators/{id}", parameters => this.UpdateStockLocator(parameters.id));
            this.Post("/inventory/stock-locators", _ => this.AddStockLocator());
        }

        private object GetStockLocators()
        {
            var resource = this.Bind<StockLocatorResource>();
            var result = this.service.GetStockLocatorsForPart(resource.PartNumber);
            return this.Negotiate.WithModel(result);
        }

        private object DeleteStockLocator()
        {
            var resource = this.Bind<StockLocatorResource>();
            return this.Negotiate.WithModel(this.service.Delete(resource.Id));
        }

        private object UpdateStockLocator(int id)
        {
            var resource = this.Bind<StockLocatorResource>();
            return this.Negotiate.WithModel(this.service.Update(id, resource));
        }

        private object AddStockLocator()
        {
            var resource = this.Bind<StockLocatorResource>();
            return this.Negotiate.WithModel(this.service.Add(resource));
        }
    }
}
