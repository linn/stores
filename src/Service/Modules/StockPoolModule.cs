namespace Linn.Stores.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Resources.StockLocators;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class StockPoolModule : NancyModule
    {
        private readonly IFacadeService<StockPool, int, StockPoolResource, StockPoolResource> stockPoolFacadeService;

        public StockPoolModule(IFacadeService<StockPool, int, StockPoolResource, StockPoolResource> stockPoolFacadeService)
        {
            this.stockPoolFacadeService = stockPoolFacadeService;
            this.Get("/inventory/stock-pools", _ => this.GetStockPools());
        }

        private object GetStockPools()
        {
            var resource = this.Bind<SearchRequestResource>();
            
            if (resource?.SearchTerm != null)
            {
                return this.Negotiate.WithModel(this.stockPoolFacadeService.Search(resource.SearchTerm));
            }

            return this.Negotiate.WithModel(this.stockPoolFacadeService.GetAll());
        }
    }
}
