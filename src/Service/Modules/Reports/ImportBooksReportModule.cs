namespace Linn.Stores.Service.Modules.Reports
{
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources.ImportBooks;
    using Linn.Stores.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class ImportBooksReportModule : NancyModule
    {
        private readonly IImportBookReportFacadeService importBookReportFacadeService;

        public ImportBooksReportModule(IImportBookReportFacadeService importBookReportFacadeService)
        {
            this.importBookReportFacadeService = importBookReportFacadeService;

            this.Get("/logistics/import-books/ipr/report", _ => this.GetIPRReport());
            this.Get("/logistics/import-books/ipr", _ => this.IPRReportOptions());
            this.Get("/logistics/import-books/ipr/report/export", _ => this.GetIPRExport());
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

        private object IPRReportOptions()
        {
            return this.Negotiate.WithModel(ApplicationSettings.Get()).WithView("Index");
        }
    }
}
