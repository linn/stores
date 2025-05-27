namespace Linn.Stores.Service.Modules
{
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Resources.StockLocators;
    using Linn.Stores.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class StoragePlaceModule : NancyModule
    {
        private readonly IAuditLocationService auditLocationService;

        private readonly IStoragePlaceService storagePlaceService;

        private readonly IStockTriggerLevelsForAStoragePlaceFacadeService triggerReportService;

        public StoragePlaceModule(
            IAuditLocationService auditLocationService,
            IStoragePlaceService storagePlaceService,
            IStockTriggerLevelsForAStoragePlaceFacadeService triggerReportService)
        {
            this.auditLocationService = auditLocationService;
            this.storagePlaceService = storagePlaceService;
            this.triggerReportService = triggerReportService;

            this.Get("/inventory/audit-locations", _ => this.GetAuditLocations());
            this.Get("/inventory/storage-places", _ => this.GetStoragePlaces());
            this.Get("/inventory/storage-place", _ => this.GetStoragePlace());
            this.Get("/inventory/storage-places/reports/stock-trigger-levels", _ => this.GetTriggerLevelsReport());
            this.Get("/inventory/storage-places/reports/stock-trigger-levels/export", _ => this.GetTriggerLevelsExport());
        }
        
        private object GetTriggerLevelsReport()
        {
            var resource = this.Bind<SearchRequestResource>();

            var results = string.IsNullOrEmpty(resource?.SearchTerm) ? null : this.triggerReportService.GetReport(resource.SearchTerm);

            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get())
                .WithView("Index");
        }

        private object GetTriggerLevelsExport()
        {
            var resource = this.Bind<SearchRequestResource>();

            var results = this.triggerReportService.GetExport(resource.SearchTerm);
            return this.Negotiate.WithModel(results).WithAllowedMediaRange("text/csv")
                .WithView("Index");
        }

        private object GetAuditLocations()
        {
            var resource = this.Bind<SearchRequestResource>();

            var results = this.auditLocationService.GetAuditLocations(resource.SearchTerm);

            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get())
                .WithView("Index");
        }

        private object GetStoragePlaces()
        {
            var resource = this.Bind<SearchRequestResource>();

            var results = this.storagePlaceService.GetStoragePlaces(resource.SearchTerm);

            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetStoragePlace()
        {
            var resource = this.Bind<StoragePlaceRequestResource>();
            var result = this.storagePlaceService.GetStoragePlace(resource);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}
