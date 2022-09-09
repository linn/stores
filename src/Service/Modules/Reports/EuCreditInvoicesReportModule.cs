namespace Linn.Stores.Service.Modules.Reports
{
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public class EuCreditInvoicesReportModule : NancyModule
    {
        private readonly IEuCreditInvoicesReportFacadeService reportService;

        public EuCreditInvoicesReportModule(IEuCreditInvoicesReportFacadeService reportService)
        {
            this.reportService = reportService;
            this.Get("/logistics/reports/eu-credit-invoices", _ => this.GetApp());
            this.Get("/logistics/reports/eu-credit-invoices/report", _ => this.GetReport());
        }

        private object GetApp()
        {
            return this.Negotiate.WithModel(ApplicationSettings.Get()).WithView("Index");
        }

        private object GetReport()
        {
            var resource = this.Bind<FromToDateResource>();
            var result = this.reportService.GetReport(resource.From, resource.To);
            return this.Negotiate.WithModel(
                    result);
        }
    }
}
