namespace Linn.Stores.Service.Modules
{
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources;
    using Linn.Stores.Service.Models;
    using Nancy;

    public sealed class SuppliersModule : NancyModule
    {
        private readonly IFacadeWithSearchReturnTen<Supplier, int, SupplierResource, SupplierResource> suppliersService;

        public SuppliersModule(IFacadeWithSearchReturnTen<Supplier, int, SupplierResource, SupplierResource> suppliersFacadeService)
        {
            this.suppliersService = suppliersFacadeService;
            this.Get("inventory/suppliers", _ => this.GetSuppliers());
        }

        private object GetSuppliers()
        {
            var results = this.suppliersService.GetAll();
            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}
