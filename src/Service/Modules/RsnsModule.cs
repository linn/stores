namespace Linn.Stores.Service.Modules
{
    using Common.Facade;
    using Domain.LinnApps;
    using Models;
    using Nancy;
    using Nancy.ModelBinding;
    using Resources;
    using Resources.RequestResources;

    public sealed class RsnsModule : NancyModule
    {
        private readonly IFacadeService<Rsn, string, RsnResource, RsnResource> rsnsService;

        public RsnsModule(IFacadeService<Rsn, string, RsnResource, RsnResource> rsnsService)
        {
            this.rsnsService = rsnsService;
            Get("logistics/rsns", _ => GetRsns());
        }

        private object GetRsns()
        {
            var resource = this.Bind<SearchRequestResource>();

            var results = rsnsService.Search(resource.SearchTerm);

            return Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}
