﻿namespace Linn.Stores.Service.Modules
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Linn.Common.Authorisation;
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Requisitions;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Resources.Requisitions;
    using Linn.Stores.Resources.StockLocators;
    using Linn.Stores.Service.Extensions;
    using Linn.Stores.Service.Models;

    using Microsoft.AspNetCore.Http;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class StockTriggerLevelsModule : NancyModule
    {
        private readonly IFacadeService<StockTriggerLevel, int, StockTriggerLevelsResource, StockTriggerLevelsResource> stockTriggerLevelsFacadeService;

        private readonly IAuthorisationService authorisationService;

        public StockTriggerLevelsModule(
            IFacadeService<StockTriggerLevel, int, StockTriggerLevelsResource, StockTriggerLevelsResource> stockTriggerLevelsFacadeService,
            IAuthorisationService authorisationService)
        {
            this.stockTriggerLevelsFacadeService = stockTriggerLevelsFacadeService;
            this.authorisationService = authorisationService;

            this.Post("/inventory/stock-trigger-levels/create", _ => this.CreateStockTriggerLevel());
            this.Get("/inventory/stock-trigger-levels/{id:int}", parameters => this.GetStockTriggerLevel(parameters.id));
            this.Get("/inventory/stock-trigger-levels", _ => this.SearchStockTriggerLevels());
            this.Put("/inventory/stock-trigger-levels/{id:int}", parameters => this.UpdateStockTriggerLevel(parameters.id));
            //this.Delete("/inventory/stock-trigger-levels/{id:int}", parameters => this.DeleteStockTriggerLevel(parameters.id));
        }

        private object SearchStockTriggerLevels()
        {
            var resource = this.Bind<SearchRequestResource>();

            var results = this.stockTriggerLevelsFacadeService.Search(resource.SearchTerm);

            return this.Negotiate.WithModel(results).WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object CreateStockTriggerLevel()
        {
            if (!this.authorisationService.HasPermissionFor(
                    AuthorisedAction.CreateStockTriggerLevel,
                    this.Context.CurrentUser.GetPrivileges()))
            {
                return new UnauthorisedResult<StockTriggerLevel>(
                    "You are not authorised to create stock trigger levels");
            }

            var resource = this.Bind<StockTriggerLevelsResource>();

            var result = this.stockTriggerLevelsFacadeService.Add(resource);

            return this.Negotiate.WithModel(result).WithMediaRangeModel("text/html", ApplicationSettings.Get);
        }

        private object UpdateStockTriggerLevel(int id)
        {
            if (!this.authorisationService.HasPermissionFor(
                    AuthorisedAction.UpdateStockTriggerLevel,
                    this.Context.CurrentUser.GetPrivileges()))
            {
                return new UnauthorisedResult<StockTriggerLevel>(
                    "You are not authorised to update stock trigger levels");
            }
            
            var resource = this.Bind<StockTriggerLevelsResource>();

            return this.Negotiate.WithModel(this.stockTriggerLevelsFacadeService.Update(id, resource));
        }

        private object GetStockTriggerLevel(int id)
        {
            var results = this.stockTriggerLevelsFacadeService.GetById(id);
            return this.Negotiate.WithModel(results).WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}
