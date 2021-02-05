namespace Linn.Stores.Service.Modules
{
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources.RequestResources;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class WarehouseModule : NancyModule
    {
        private readonly IWarehouseFacadeService warehouseFacadeService;

        public WarehouseModule(IWarehouseFacadeService warehouseFacadeService)
        {
            this.warehouseFacadeService = warehouseFacadeService;
            this.Post("/logistics/wcs/move-all-to-upper", _  => this.MoveAllPalletsToUpper());
            this.Post("/logistics/wcs/move-to-upper", _  => this.MovePalletToUpper());
        }

        private object MovePalletToUpper()
        {
            var resource = this.Bind<PalletMoveRequestResource>();

            return this.Negotiate.WithModel(
                this.warehouseFacadeService.MovePalletToUpper(resource.PalletNumber, resource.Reference));
        }

        private object MoveAllPalletsToUpper()
        {
            return this.Negotiate.WithModel(this.warehouseFacadeService.MoveAllPalletsToUpper());
        }
    }
}
