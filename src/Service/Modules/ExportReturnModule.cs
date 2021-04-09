namespace Linn.Stores.Service.Modules
{
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class ExportReturnModule : NancyModule
    {
        private readonly IExportReturnService exportReturnService;

        public ExportReturnModule(IExportReturnService exportReturnService)
        {
            this.exportReturnService = exportReturnService;
            this.Post("/inventory/exports/returns", parameters => this.CreateExportReturn());
            this.Put("/inventory/exports/returns/{id}", parameters => this.UpdateExportReturn(parameters.id));
            this.Get("/inventory/exports/returns/{id}", parameters => this.GetExportReturn(parameters.id));
            this.Post(
                "/inventory/exports/returns/make-intercompany-invoices",
                _ => this.MakeIntercompanyInvoices());
        }

        private object CreateExportReturn()
        {
            var resource = this.Bind<MakeExportReturnRequestResource>();

            var result = this.exportReturnService.MakeExportReturn(resource.Rsns, resource.HubReturn);

            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get())
                .WithView("Index");
        }

        private object UpdateExportReturn(int id)
        {
            var resource = this.Bind<ExportReturnResource>();

            var result = this.exportReturnService.UpdateExportReturn(id, resource);

            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get())
                .WithView("Index");
        }

        private object GetExportReturn(int id)
        {
            var result = this.exportReturnService.GetById(id);

            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get())
                .WithView("Index");
        }

        private object MakeIntercompanyInvoices()
        {
            var resource = this.Bind<MakeIntercompanyInvoicesRequestResource>();

            var result = this.exportReturnService.MakeIntercompanyInvoices(resource.ReturnId);

            return this.Negotiate.WithModel(result).WithMediaRangeModel("text/html", ApplicationSettings.Get())
                .WithView("Index");
        }
    }
}