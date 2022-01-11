namespace Linn.Stores.Service.Modules.Reports
{
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources.ImportBooks;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class ImportBooksReportModule : NancyModule
    {
        private readonly IImportBookReportFacadeService importBookReportFacadeService;

        private readonly IZeroValuedInvoiceDetailsReportFacadeService zeroValuedInvoiceDetailsService;

        public ImportBooksReportModule(
            IImportBookReportFacadeService importBookReportFacadeService,
            IZeroValuedInvoiceDetailsReportFacadeService zeroValuedInvoiceDetailsService)
        {
            this.importBookReportFacadeService = importBookReportFacadeService;
            this.Get("/logistics/import-books/ipr", _ => this.GetApp());
            this.Get("/logistics/import-books/ipr/report", _ => this.GetIPRReport());
            this.Get("/logistics/import-books/ipr/report/export", _ => this.GetIPRExport());
            this.Get("/logistics/import-books/eu", _ => this.GetApp());
            this.Get("/logistics/import-books/eu/report", _ => this.GetEUReport());
            this.Get("/logistics/import-books/eu/report/export", _ => this.GetEUExport());

            this.zeroValuedInvoiceDetailsService = zeroValuedInvoiceDetailsService;
            this.Get("logistics/zero-valued-invoiced-report", _ => this.GetZeroValuedInvoicedItemsReport());
        }

        private object GetIPRReport()
        {
            var resource = this.Bind<IPRSearchResource>();

            var results = this.importBookReportFacadeService.GetImpbookIPRReport(resource);
            return this.Negotiate.WithModel(results).WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetIPRExport()
        {
            var resource = this.Bind<IPRSearchResource>();

            var results = this.importBookReportFacadeService.GetImpbookIprReportExport(resource);
            return this.Negotiate.WithModel(results).WithAllowedMediaRange("text/csv")
                .WithView("Index");
        }

        private object GetEUReport()
        {
            var resource = this.Bind<EUSearchResource>();

            var results = this.importBookReportFacadeService.GetImpbookEuReport(resource);
            return this.Negotiate.WithModel(results).WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetEUExport()
        {
            var resource = this.Bind<EUSearchResource>();

            var results = this.importBookReportFacadeService.GetImpbookEuReportExport(resource);
            return this.Negotiate.WithModel(results).WithAllowedMediaRange("text/csv")
                .WithView("Index");
        }

        private object GetZeroValuedInvoicedItemsReport()
        {
            var resource = this.Bind<FromToDateResource>();

            var results = this.zeroValuedInvoiceDetailsService.GetCsvReport(resource.From, resource.To);
            return this.Negotiate.WithModel(results).WithAllowedMediaRange("text/csv")
                .WithView("Index");
        }

        private object GetApp()
        {
            return this.Negotiate.WithModel(ApplicationSettings.Get()).WithView("Index");
        }
    }
}
