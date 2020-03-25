namespace Linn.Stores.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;
    using Linn.Stores.Service.Models;
    using Nancy;
    using Nancy.ModelBinding;

    public sealed class CarriersModule : NancyModule
    {
        private readonly IFacadeService<Carrier, int, CarrierResource, CarrierResource> carriersFacadeService;

        public CarriersModule(IFacadeService<Carrier, int, CarrierResource, CarrierResource> carriersFacadeService)
        {
            this.carriersFacadeService = carriersFacadeService;
            this.Get("/logistics/carriers", _ => this.GetCarriers());
        }

        private object GetCarriers()
        {
            var resource = this.Bind<SearchRequestResource>();
            var results = string.IsNullOrEmpty(resource.SearchTerm)
                              ? this.carriersFacadeService.GetAll()
                              : this.carriersFacadeService.Search(resource.SearchTerm);
            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}
