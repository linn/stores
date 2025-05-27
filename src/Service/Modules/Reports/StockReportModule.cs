namespace Linn.Stores.Service.Modules.Reports
{
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class StockReportModule : NancyModule
    {
        private readonly IStockReportFacadeService reportService;

        public StockReportModule(IStockReportFacadeService reportService)
        {
            this.reportService = reportService;
            this.Get("/inventory/reports/stock-locators-report", _ => this.GetApp());
            this.Get("/inventory/reports/stock-locators-report/report", _ => this.GetReport());
            this.Get("/inventory/reports/stock-locators-report/export", _ => this.GetExport());
        }

        private object GetApp()
        {
            return this.Negotiate.WithModel(ApplicationSettings.Get()).WithView("Index");
        }

        private object GetExport()
        {
            var resource = this.Bind<StockLocatorReportRequestResource>();
            var results = this.reportService.GetStockLocatorReportExport(resource);
            return this.Negotiate.WithModel(results).WithAllowedMediaRange("text/csv").WithView("Index");
        }

        private object GetReport()
        {
            var resource = this.Bind<StockLocatorReportRequestResource>();
            var results = this.reportService.GetStockLocatorReport(resource);

            return this.Negotiate.WithModel(results).WithMediaRangeModel("text/html", ApplicationSettings.Get())
                .WithView("Index");
        }
    }
}
