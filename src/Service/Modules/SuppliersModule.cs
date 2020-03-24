namespace Linn.Stores.Service.Modules
{
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Service.Models;

    using Nancy;

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
            var results = this.suppliersService.GetSuppliers();
            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}