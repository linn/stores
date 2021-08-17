namespace Linn.Stores.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.GoodsIn;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Resources.StockLocators;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class GoodsInModule : NancyModule
    {
        private readonly IGoodsInFacadeService service;

        private readonly IFacadeFilterService<StorageLocation, int,
            StorageLocationResource, StorageLocationResource, StorageLocationResource> storageLocationService;

        private readonly ISalesArticleService salesArticleService;

        public GoodsInModule(
            IGoodsInFacadeService service,
            IFacadeFilterService<StorageLocation, int, StorageLocationResource, StorageLocationResource, StorageLocationResource> storageLocationService,
            ISalesArticleService salesArticleService)
        {
            this.service = service;
            this.storageLocationService = storageLocationService;
            this.salesArticleService = salesArticleService;
            this.Post("/logistics/book-in", _ => this.DoBookIn());
            this.Get("/logistics/loan-details", _ => this.GetLoanDetails());
            this.Get("/logistics/goods-in/dem-locations", _ => this.GetDemLocations());
            this.Get("/inventory/sales-articles", _ => this.SearchSalesArticles());
            this.Get("/logistics/purchase-orders/validate/{id}", parameters => this.ValidatePurchaseOrder(parameters.id));
            this.Get("/logistics/purchase-orders/validate-qty", _ => this.ValidatePurchaseOrderBookInQty());
        }

        private object DoBookIn()
        {
            var resource = this.Bind<BookInRequestResource>();
            return this.Negotiate
                .WithModel(
                    this.service.DoBookIn(resource));
        }

        private object GetLoanDetails()
        {
            return this.Negotiate.WithModel(
                this.service.GetLoanDetails(this.Bind<LoanDetailResource>().LoanNumber));
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

        private object SearchSalesArticles()
        {
            return this.Negotiate.WithModel(
                this.salesArticleService.SearchLiveSalesArticles(
                    this.Bind<SearchRequestResource>().SearchTerm));
        }

        private object ValidatePurchaseOrder(int id)
        {
            return this.Negotiate.WithModel(
                this.service.ValidatePurchaseOrder(
                    id, 1));
        }

        private object ValidatePurchaseOrderBookInQty()
        {
            var resource = this.Bind<ValidateBookInPurchaseOrderQtyRequestResource>();
            var orderLine = resource.OrderLine ?? 1;
            return this.Negotiate.WithModel(
                this.service.ValidatePurchaseOrderQty(resource.OrderNumber, orderLine, resource.Qty));
        }
    }
}
