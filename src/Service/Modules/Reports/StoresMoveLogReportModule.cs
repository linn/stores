namespace Linn.Stores.Service.Modules.Reports
{
    using Linn.Stores.Facade.Services;

    using Nancy;

    using Linn.Stores.Service.Models;
    using Linn.Stores.Resources.RequestResources;

    using Nancy.ModelBinding;

    public class StoresMoveLogReportModule : NancyModule
    {
        private readonly IStoresMoveLogReportFacadeService reportService;

        public StoresMoveLogReportModule(IStoresMoveLogReportFacadeService reportService)
        {
            this.reportService = reportService;
            this.Get("/inventory/reports/stores-move-log", _ => this.GetOptions());
            this.Get("/inventory/reports/stores-move-log/report", _ => this.GetReport());
            this.Get("/inventory/reports/stores-move-log/export", _ => this.GetExport());

            // http://localhost:3000/inventory/reports/stores-move-log/report
        }

        private object GetOptions()
        {
            return this.Negotiate.WithModel(ApplicationSettings.Get()).WithView("Index");
        }

        private object GetExport()
        {
            var resource = this.Bind<StoresMoveLogReportRequestResource>();
            var results = this.reportService.GetExport(resource);
            return this.Negotiate.WithModel(results).WithAllowedMediaRange("text/csv").WithView("Index");
        }

        private object GetReport()
        {
            var resource = this.Bind<StoresMoveLogReportRequestResource>();
            var results = this.reportService.GetReport(resource);

            return this.Negotiate.WithModel(results).WithMediaRangeModel("text/html", ApplicationSettings.Get())
                .WithView("Index");
        }
    }
}
