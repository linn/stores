namespace Linn.Stores.Service.Modules
{
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources;
    using Linn.Stores.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class SuppliersModule : NancyModule
    {
        private readonly ISuppliersService suppliersService;

        public SuppliersModule(ISuppliersService suppliersFacadeService)
        {
            this.suppliersService = suppliersFacadeService;
            this.Get("inventory/suppliers", _ => this.GetSuppliers());
        }

        private object GetSuppliers()
        {
            var resource = this.Bind<SearchRequestResource>();

            var results = this.suppliersService.GetSuppliers(resource.SearchTerm);
            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}