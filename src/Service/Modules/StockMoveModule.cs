﻿namespace Linn.Stores.Service.Modules
{
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources.RequestResources;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class StockMoveModule : NancyModule
    {
        private readonly IAvailableStockFacadeService availableStockFacadeService;

        public StockMoveModule(IAvailableStockFacadeService availableStockFacadeService)
        {
            this.availableStockFacadeService = availableStockFacadeService;
            this.Get("/inventory/available-stock", _ => this.GetAvailableStock());
            this.Post("/inventory/move-stock", _ => this.MoveStock());
        }

        private object MoveStock()
        {
            throw new System.NotImplementedException();
        }

        private object GetAvailableStock()
        {
            var resource = this.Bind<SearchRequestResource>();
            return this.Negotiate.WithModel(this.availableStockFacadeService.GetAvailableStock(resource.SearchTerm));
        }
    }
}
