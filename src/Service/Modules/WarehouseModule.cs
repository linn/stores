namespace Linn.Stores.Service.Modules
{
    using System;
    using System.Linq;

    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Resources.Scs;
    using Linn.Stores.Service.Extensions;

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
            this.Post("/logistics/wcs/warehouse-tasks", _ => this.AddWarehouseTask());
            this.Get("/logistics/wcs/warehouse-pallets/{palletId:int}", parameters => this.GetWarehousePallet(parameters.palletId));
            this.Get("/logistics/wcs/warehouse-pallets/scs", _ => this.QueryScsPallets());
            this.Post("/logistics/wcs/warehouse-pallets/scs", _ => this.PostScsPallets());
            this.Get("/logistics/wcs/warehouse-pallets", _ => this.QueryWarehousePallet());
        }

        private object MovePalletToUpper()
        {
            var resource = this.Bind<PalletMoveRequestResource>();

            return this.Negotiate.WithModel(
                this.warehouseFacadeService.MovePalletToUpper(resource.PalletNumber, resource.PickingReference));
        }

        private object MoveAllPalletsToUpper()
        {
            return this.Negotiate.WithModel(this.warehouseFacadeService.MoveAllPalletsToUpper());
        }

        private object AddWarehouseTask()
        {
            var resource = this.Bind<WarehouseTaskResource>();
            resource.EmployeeId = Convert.ToInt32(this.Context.CurrentUser.GetEmployeeUri().Split('/').Last());

            return this.Negotiate.WithModel(
                this.warehouseFacadeService.MakeWarehouseTask(resource));
        }

        private object GetWarehousePallet(int palletId)
        {
            var result = this.warehouseFacadeService.GetPalletLocation(palletId);

            return this.Negotiate
                .WithModel(result);
        }

        private object QueryWarehousePallet()
        {
            var resource = this.Bind<SearchWarehousePallet>();
            var result = this.warehouseFacadeService.GetPalletAtLocation(resource.LocRef);

            return this.Negotiate
                .WithModel(result);
        }

        private object QueryScsPallets()
        {
            var result = this.warehouseFacadeService.GetScsPallets();

            return this.Negotiate
                .WithModel(result);
        }

        private object PostScsPallets()
        {
            var resource = this.Bind<ScsPalletsResource>();

            return this.Negotiate.WithModel(
                this.warehouseFacadeService.StorePallets(resource));
        }
    }
}
