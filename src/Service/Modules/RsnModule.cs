namespace Linn.Stores.Service.Modules
{
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class RsnModule : NancyModule
    {
        private readonly IRsnService rsnService;

        public RsnModule(IRsnService rsnService)
        {
            this.rsnService = rsnService;
            this.Get("/inventory/rsns", parameters => this.GetRsns());
        }

        private object GetRsns()
        {
            var resource = this.Bind<RsnSearchRequestResource>();

            var results = this.rsnService.SearchRsns(resource.AccountId, resource.OutletNumber);

            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get())
                .WithView("Index");
        }
    }
}