namespace Linn.Stores.Service.Modules
{
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class RootProductsModule : NancyModule
    {
        private readonly IRootProductService rootProductsService;

        public RootProductsModule(IRootProductService rootProductsFacadeService)
        {
            this.rootProductsService = rootProductsFacadeService;
            this.Get("inventory/root-products", _ => this.GetRootProducts());
        }

        private object GetRootProducts()
        {
            var resource = this.Bind<SearchRequestResource>();

            var results = this.rootProductsService.GetValid(resource.SearchTerm);
            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}
