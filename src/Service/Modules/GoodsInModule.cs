namespace Linn.Stores.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Resources.StockLocators;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class GoodsInModule : NancyModule
    {
        private readonly IGoodsInFacadeService service;

        private readonly IFacadeFilterService<StorageLocation, int,
            StorageLocationResource, StorageLocationResource, StorageLocationResource> storageLocationService;

        public GoodsInModule(
            IGoodsInFacadeService service,
            IFacadeFilterService<StorageLocation, int, StorageLocationResource, StorageLocationResource, StorageLocationResource> storageLocationService)
        {
            this.service = service;
            this.storageLocationService = storageLocationService;
            this.Post("/logistics/book-in", _ => this.DoBookIn());
            this.Get("/logistics/loan-details", _ => this.GetLoanDetails());
            this.Get("/logistics/goods-in/dem-locations", _ => this.GetDemLocations());
        }

        private object DoBookIn()
        {
            var resource = this.Bind<BookInRequestResource>();
            var result = this.service.DoBookIn(resource);
            return this.Negotiate.WithModel(this.service.DoBookIn(resource));
        }

        private object GetLoanDetails()
        {
            var resource = this.Bind<LoanDetailResource>();
            var result = this.service.GetLoanDetails(resource.LoanNumber);
            return this.Negotiate.WithModel(result);
        }

        private object GetDemLocations()
        {
            return this.Negotiate.WithModel(
                this.storageLocationService.
                    FilterBy(new StorageLocationResource
                            {
                                DefaultStockPool = "DEM STOCK"
                            }));
        }
    }
}
