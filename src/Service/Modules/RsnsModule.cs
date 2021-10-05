namespace Linn.Stores.Service.Modules
{
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class RsnsModule : NancyModule
    {
        private readonly IRsnService rsnsService;

        public RsnsModule(IRsnService rsnsService)
        {
            this.rsnsService = rsnsService;
            this.Get("logistics/rsns", _ => this.GetRsns());
        }

        private object GetRsns()
        {
            var resource = this.Bind<SearchRequestResource>();

            var results = this.rsnsService.Search(resource.SearchTerm);

            return this.Negotiate.WithModel(results).WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}
