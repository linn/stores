namespace Linn.Stores.Service.Modules
{
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Service.Models;
    using Nancy;
    using Nancy.ModelBinding;

    public sealed class CarriersModule : NancyModule
    {
        private readonly ICarriersService carriersService;

        public CarriersModule(ICarriersService carriersService)
        {
            this.carriersService = carriersService;
            this.Get("/logistics/carriers", _ => this.GetCarriers());
        }

        private object GetCarriers()
        {
            var resource = this.Bind<SearchRequestResource>();

            var results = this.carriersService.SearchCarriers(resource.SearchTerm);

            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}
