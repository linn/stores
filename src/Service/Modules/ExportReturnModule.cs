namespace Linn.Stores.Service.Modules
{
    using Linn.Stores.Facade.Services;
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
            this.Get("/inventory/exports/rsns", parameters => this.GetRsns());
            this.Post("/inventory/exports/returns", parameters => this.CreateExportReturn());
            this.Get("/inventory/exports/returns/{id}", parameters => this.GetExportReturn(parameters.id));
        }

        private object GetRsns()
        {
            var resource = this.Bind<ExportRsnSearchRequestResource>();

            var results = this.exportReturnService.SearchRsns(resource.AccountId, resource.OutletNumber);

            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get())
                .WithView("Index");
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

        private object GetExportReturn(int id)
        {
            var result = this.exportReturnService.GetById(id);

            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get())
                .WithView("Index");
        }
    }
}