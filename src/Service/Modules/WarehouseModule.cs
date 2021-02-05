namespace Linn.Stores.Service.Modules
{
    using Linn.Stores.Facade.Services;

    using Nancy;

    public sealed class WarehouseModule : NancyModule
    {
        private readonly IWarehouseFacadeService warehouseFacadeService;

        public WarehouseModule(IWarehouseFacadeService warehouseFacadeService)
        {
            this.warehouseFacadeService = warehouseFacadeService;
            this.Post("/logistics/wcs/move-all-to-upper", _  => this.MoveAllPalletsToUpper());
        }

        private object MoveAllPalletsToUpper()
        {
            return this.Negotiate.WithModel(this.warehouseFacadeService.MoveAllPalletsToUpper());
        }
    }
}
