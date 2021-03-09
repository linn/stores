namespace Linn.Stores.Service.Modules
{
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class ExportRsnModule : NancyModule
    {
        private readonly IExportRsnService exportRsnService;

        public ExportRsnModule(IExportRsnService exportRsnService)
        {
            this.exportRsnService = exportRsnService;
            this.Get("/inventory/exports/rsns", parameters => this.GetRsns());
        }

        private object GetRsns()
        {
            var resource = this.Bind<ExportRsnSearchRequestResource>();

            var results = this.exportRsnService.SearchRsns(resource.AccountId, resource.OutletNumber);

            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get())
                .WithView("Index");
        }
    }
}