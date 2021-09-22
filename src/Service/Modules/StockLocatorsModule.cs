namespace Linn.Stores.Service.Modules
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Resources.StockLocators;
    using Linn.Stores.Service.Extensions;
    using Linn.Stores.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class StockLocatorsModule : NancyModule
    {
        private readonly IStockLocatorFacadeService service;

        private readonly IFacadeFilterService<StorageLocation, int, StorageLocationResource, StorageLocationResource, StorageLocationResource>
            storageLocationService;

        private readonly IFacadeService<InspectedState, string, InspectedStateResource, InspectedStateResource>
            inspectedStateService;

        private readonly IStockQuantitiesService stockQuantitiesService;

        private readonly IStockLocatorPricesService pricesService;

        public StockLocatorsModule(
            IStockLocatorFacadeService service,
            IFacadeFilterService<StorageLocation, int, StorageLocationResource, StorageLocationResource, StorageLocationResource> storageLocationService,
            IFacadeService<InspectedState, string, InspectedStateResource, InspectedStateResource> inspectedStateService,
            IStockQuantitiesService stockQuantitiesService,
            IStockLocatorPricesService pricesService)
        {
            this.service = service;
            this.storageLocationService = storageLocationService;
            this.inspectedStateService = inspectedStateService;
            this.stockQuantitiesService = stockQuantitiesService;
            this.pricesService = pricesService;

            this.Get("/inventory/stock-locators", _ => this.GetStockLocators());
            this.Get("/inventory/stock-locators/batches", _ => this.GetBatches());
            this.Delete("/inventory/stock-locators/{id}", parameters => this.DeleteStockLocator(parameters.id));
            this.Put("/inventory/stock-locators/{id}", parameters => this.UpdateStockLocator(parameters.id));
            this.Get("/inventory/storage-locations", _ => this.GetStorageLocations());
            this.Post("/inventory/stock-locators", _ => this.AddStockLocator());
            this.Get("/inventory/stock-locators/states", _ => this.GetStates());
            this.Get("/inventory/stock-locators-by-location/", _ => this.GetStockLocatorsByLocation());
            this.Get("/inventory/stock-quantities/", _ => this.GetStockQuantities());
            this.Get("/inventory/stock-locators/prices", _ => this.GetPrices());
            this.Get("/inventory/stock-locators/stock-moves", _ => this.GetMoves());
        }

        private object GetStockLocators()
        {
            var resource = this.Bind<StockLocatorResource>();
            if (resource.PartId != null)
            {
                return this.Negotiate.WithModel(this.service.GetStockLocatorsForPart((int)resource.PartId))
                    .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                    .WithView("Index");
            }

            return this.Negotiate.WithModel(new BadRequestResult<IEnumerable<StockLocatorPrices>>("No part number supplied"));
        }

        private object GetStockLocatorsByLocation()
        {
            var resource = this.Bind<StockLocatorQueryResource>();
            var result = this.service.GetStockLocations(resource);
            return this.Negotiate.WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetBatches()
        {
            var resource = this.Bind<SearchRequestResource>();
            var result = this.service.GetBatches(resource.SearchTerm);
            return this.Negotiate.WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetStates()
        {
            var result = this.inspectedStateService.GetAll();
            return this.Negotiate.WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetStorageLocations()
        {
            var resource = this.Bind<SearchRequestResource>();
            var result = this.storageLocationService.Search(resource.SearchTerm);
            return this.Negotiate.WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object DeleteStockLocator(int id)
        {
            var resource = new StockLocatorResource
                               {
                                   Id = id, UserPrivileges = this.Context.CurrentUser.GetPrivileges()
                               };
       
            return this.Negotiate.WithModel(this.service.Delete(resource));
        }

        private object UpdateStockLocator(int id)
        {
            var resource = this.Bind<StockLocatorResource>();
            resource.UserPrivileges = this.Context.CurrentUser.GetPrivileges();
            return this.Negotiate.WithModel(this.service.Update(id, resource));
        }

        private object GetStockQuantities()
        {
            var resource = this.Bind<StockLocatorResource>();
            return this.stockQuantitiesService.GetStockQuantities(resource.PartNumber);
        }

        private object AddStockLocator()
        {
            var resource = this.Bind<StockLocatorResource>();
            resource.UserPrivileges = this.Context.CurrentUser.GetPrivileges();
            return this.Negotiate.WithModel(this.service.Add(resource));
        }

        private object GetPrices()
        {
            var resource = this.Bind<StockLocatorResource>();

            return this.Negotiate.WithModel(this.pricesService.GetPrices(resource));
        }

        private object GetMoves()
        {
            var resource = this.Bind<StockMovesRequestResource>();
            var result = this.service.GetMoves(resource.SearchTerm, resource.PalletNumber, resource.LocationId);
            return this.Negotiate.WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}
