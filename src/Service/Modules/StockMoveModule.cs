namespace Linn.Stores.Service.Modules
{
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources.RequestResources;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class StockMoveModule : NancyModule
    {
        private readonly IStockAvailableFacadeService stockAvailableFacadeService;

        public StockMoveModule(IStockAvailableFacadeService stockAvailableFacadeService)
        {
            this.stockAvailableFacadeService = stockAvailableFacadeService;
            this.Get("/inventory/available-stock", _ => this.GetAvailableStock());
        }

        private object GetAvailableStock()
        {
            var resource = this.Bind<PartNumberRequestResource>();
            return this.Negotiate.WithModel(this.stockAvailableFacadeService.GetAvailableStock(resource.PartNumber));
        }
    }
}
