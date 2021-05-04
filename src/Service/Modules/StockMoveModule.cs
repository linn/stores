namespace Linn.Stores.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class StockMoveModule : NancyModule
    {
        private readonly IAvailableStockFacadeService availableStockFacadeService;

        private readonly IMoveStockFacadeService moveStockFacadeService;

        private readonly IFacadeService<PartStorageType, int, PartStorageTypeResource, PartStorageTypeResource> partStorageTypeFacadeService;

        public StockMoveModule(
            IAvailableStockFacadeService availableStockFacadeService,
            IMoveStockFacadeService moveStockFacadeService,
            IFacadeService<PartStorageType, int, PartStorageTypeResource, PartStorageTypeResource> partStorageTypeFacadeService)
        {
            this.availableStockFacadeService = availableStockFacadeService;
            this.moveStockFacadeService = moveStockFacadeService;
            this.partStorageTypeFacadeService = partStorageTypeFacadeService;
            this.Get("/inventory/available-stock", _ => this.GetAvailableStock());
            this.Get("/inventory/move-stock", _ => this.GetApp());
            this.Post("/inventory/move-stock", _ => this.MoveStock());
            this.Get("/inventory/part-storage-types", _ => this.GetPartStorageTypes());
        }

        private object GetPartStorageTypes()
        {
            var resource = this.Bind<SearchRequestResource>();
            return this.Negotiate.WithModel(this.partStorageTypeFacadeService.Search(resource.SearchTerm));
        }

        private object MoveStock()
        {
            var resource = this.Bind<MoveStockRequestResource>();
            return this.Negotiate.WithModel(this.moveStockFacadeService.MoveStock(resource));
        }

        private object GetAvailableStock()
        {
            var resource = this.Bind<SearchRequestResource>();
            return this.Negotiate.WithModel(this.availableStockFacadeService.GetAvailableStock(resource.SearchTerm));
        }

        private object GetApp()
        {
            return this.Negotiate.WithModel(ApplicationSettings.Get()).WithView("Index");
        }
    }
}
