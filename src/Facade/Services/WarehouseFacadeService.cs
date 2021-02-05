namespace Linn.Stores.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;

    public class WarehouseFacadeService : IWarehouseFacadeService
    {
        private readonly IWarehouseService warehouseService;

        public WarehouseFacadeService(IWarehouseService warehouseService)
        {
            this.warehouseService = warehouseService;
        }

        public IResult<string> MoveAllPalletsToUpper()
        {
            return new SuccessResult<string>(this.warehouseService.MoveAllPalletsToUpper());
        }
    }
}
