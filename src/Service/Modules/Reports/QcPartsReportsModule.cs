namespace Linn.Stores.Service.Modules.Reports
{
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class QcPartsReportModule : NancyModule
    {
        private readonly IQcPartsReportFacadeService reportService;

        public QcPartsReportModule(IQcPartsReportFacadeService reportService)
        {
            this.reportService = reportService;
            this.Get("/inventory/reports/qc-parts", _ => this.GetReport());
            this.Get("/inventory/reports/qc-parts/export", _ => this.GetExport());
        }

        private object GetExport()
        {
            var results = this.reportService.GetExport();
            return this.Negotiate.WithModel(results).WithAllowedMediaRange("text/csv")
                .WithView("Index");
        }

        private object GetReport()
        {

            var results = this.reportService.GetReport();

            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get())
                .WithView("Index");
        }
    }
}
