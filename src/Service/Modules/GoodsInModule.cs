﻿namespace Linn.Stores.Service.Modules
{
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.GoodsIn;
    using Linn.Stores.Resources.MessageDispatch;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Resources.StockLocators;
    using Linn.Stores.Service.Extensions;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class GoodsInModule : NancyModule
    {
        private readonly IGoodsInFacadeService service;

        private readonly IFacadeFilterService<StorageLocation, int,
            StorageLocationResource, StorageLocationResource, StorageLocationResource> storageLocationService;

        private readonly ISalesArticleService salesArticleService;

        private readonly IRsnConditionsService rsnConditionsService;

        private readonly IRsnAccessoriesService rsnAccessoriesService;

        public GoodsInModule(
            IGoodsInFacadeService service,
            IFacadeFilterService<StorageLocation, int, StorageLocationResource, StorageLocationResource, StorageLocationResource> storageLocationService,
            ISalesArticleService salesArticleService,
            IRsnConditionsService rsnConditionsService,
            IRsnAccessoriesService rsnAccessoriesService)
        {
            this.service = service;
            this.storageLocationService = storageLocationService;
            this.salesArticleService = salesArticleService;
            this.Post("/logistics/book-in", _ => this.DoBookIn());
            this.Get("/logistics/loan-details", _ => this.GetLoanDetails());
            this.Get("/logistics/goods-in/dem-locations", _ => this.GetDemLocations());
            this.Get("/inventory/sales-articles", _ => this.SearchSalesArticles());
            this.Get("/logistics/purchase-orders/validate/{id}", parameters => this.ValidatePurchaseOrder(parameters.id));
            this.Get("/logistics/rsn/validate/{id}", parameters => this.ValidateRsn(parameters.id));

            this.Get("/logistics/purchase-orders/validate-qty", _ => this.ValidatePurchaseOrderBookInQty());
            this.Post("/logistics/goods-in/print-labels", _ => this.PrintLabels());
            this.Post("/logistics/goods-in/print-rsn", _ => this.PrintRsn());
            this.Get("/logistics/goods-in/validate-storage-type", _ => this.ValidateStorageType());

            this.rsnAccessoriesService = rsnAccessoriesService;
            this.rsnConditionsService = rsnConditionsService;
            this.Get("/logistics/goods-in/rsn-conditions", _ => this.GetRsnConditions());
            this.Get("/logistics/goods-in/rsn-accessories", _ => this.GetRsnAccessories());
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

        private object ValidateStorageType()
        {
            var resource = this.Bind<ValidateStorageTypeRequestResource>();
            var result = this.service.ValidateStorageType(resource);
            return this.Negotiate.WithModel(result);
        }

        private object PrintLabels()
        {
            var resource = this.Bind<PrintGoodsInLabelsRequestResource>();
            var closedByUri = this.Context.CurrentUser.GetEmployeeUri();
            resource.UserNumber = int.Parse(closedByUri.Split("/").Last());
            return this.Negotiate.WithModel(this.service.PrintGoodsInLabels(resource));
        }

        private object PrintRsn()
        {
            var resource = this.Bind<PrintRsnMessageResource>();
            var userNumber = int.Parse(this.Context.CurrentUser.GetEmployeeUri().Split("/").Last());

            return this.Negotiate.WithModel(this.service.PrintRsn(resource.RsnNumber, userNumber));
        }

        private object GetRsnAccessories()
        {
            return this.Negotiate.WithModel(this.rsnAccessoriesService.GetRsnAccessories());
        }

        private object GetRsnConditions()
        {
            return this.Negotiate.WithModel(this.rsnConditionsService.GetRsnConditions());
        }

        private object ValidateRsn(int rsnNumber)
        {
            return this.Negotiate.WithModel(this.service.ValidateRsn(rsnNumber));
        }
    }
}
