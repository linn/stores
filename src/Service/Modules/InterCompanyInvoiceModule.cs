namespace Linn.Stores.Service.Modules
{
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class InterCompanyInvoiceModule : NancyModule
    {
        private readonly IInterCompanyInvoiceService interCompanyInvoiceService;

        public InterCompanyInvoiceModule(IInterCompanyInvoiceService interCompanyInvoiceService)
        {
            this.interCompanyInvoiceService = interCompanyInvoiceService;
            this.Get("/inventory/exports/inter-company-invoices", _ => this.GetInterCompanyInvoices());
            this.Get("/inventory/exports/inter-company-invoices/{id}", parameters => this.GetInterCompanyInvoice(parameters.id));

        }

        private object GetInterCompanyInvoices()
        {
            var resource = this.Bind<SearchRequestResource>();

            var result = this.interCompanyInvoiceService.SearchInterCompanyInvoices(resource.SearchTerm);

            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get())
                .WithView("Index");
        }

        private object GetInterCompanyInvoice(int id)
        {
            var result = this.interCompanyInvoiceService.GetByDocumentNumber(id);

            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get())
                .WithView("Index");
        }
    }
}