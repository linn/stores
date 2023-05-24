namespace Linn.Stores.Service.Modules
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Authorisation;
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Service.Extensions;
    using Linn.Stores.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class StockTriggerLevelsModule : NancyModule
    {
        private readonly IStockTriggerLevelsFacadeService stockTriggerLevelsFacadeService;

        private readonly IAuthorisationService authorisationService;

        public StockTriggerLevelsModule(
            IStockTriggerLevelsFacadeService stockTriggerLevelsFacadeService,
            IAuthorisationService authorisationService)
        {
            this.stockTriggerLevelsFacadeService = stockTriggerLevelsFacadeService;
            this.authorisationService = authorisationService;

            this.Post("/inventory/stock-trigger-levels/", _ => this.CreateStockTriggerLevel());
            this.Get("/inventory/stock-trigger-levels/{id:int}", parameters => this.GetStockTriggerLevel(parameters.id));
            this.Get("/inventory/stock-trigger-levels/", _ => this.SearchStockTriggerLevels());
            this.Put("/inventory/stock-trigger-levels/{id:int}", parameters => this.UpdateStockTriggerLevel(parameters.id));
            this.Delete("/inventory/stock-trigger-levels/{id:int}", parameters => this.DeleteStockTriggerLevel(parameters.id));
        }

        private object CreateStockTriggerLevel()
        {
            if (!this.authorisationService.HasPermissionFor(
                    AuthorisedAction.CreateStockTriggerLevel,
                    this.Context.CurrentUser.GetPrivileges()))
            {
                return new BadRequestResult<StockTriggerLevel>(
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
                return new BadRequestResult<StockTriggerLevel>(
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

        private object SearchStockTriggerLevels()
        {
            var resource = this.Bind<StockTriggerLevelsSearchRequestResource>();
            IResult<IEnumerable<StockTriggerLevel>> results;

            if (!string.IsNullOrEmpty(resource.PartNumberSearchTerm)
                || !string.IsNullOrEmpty(resource.StoragePlaceSearchTerm))
            {
                results = this.stockTriggerLevelsFacadeService.SearchStockTriggerLevelsWithWildcard(
                    resource.PartNumberSearchTerm,
                    resource.StoragePlaceSearchTerm);
            }
            else
            {
                results = this.stockTriggerLevelsFacadeService.GetAll();
            }

            return this.Negotiate.WithModel(results).WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object DeleteStockTriggerLevel(int id)
        {
            if (!this.authorisationService.HasPermissionFor(
                    AuthorisedAction.CreateStockTriggerLevel,
                    this.Context.CurrentUser.GetPrivileges()))
            {
                return new BadRequestResult<StockTriggerLevel>(
                    "You are not authorised to create stock trigger levels");
            }

            var userNumber = int.Parse(this.Context.CurrentUser.GetEmployeeUri().Split("/").Last());

            return this.Negotiate.WithModel(this.stockTriggerLevelsFacadeService.DeleteStockTriggerLevel(id, userNumber));
        }
    }
}
