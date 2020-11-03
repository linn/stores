namespace Linn.Stores.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    using Nancy;

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
            return this.Negotiate.WithModel(this.stockPoolFacadeService.GetAll());
        }
    }
}