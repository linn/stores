namespace Linn.Stores.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Models;

    public class WarehouseFacadeService : IWarehouseFacadeService
    {
        private readonly IWarehouseService warehouseService;

        public WarehouseFacadeService(IWarehouseService warehouseService)
        {
            this.warehouseService = warehouseService;
        }

        public IResult<MessageResult> MoveAllPalletsToUpper()
        {
            return new SuccessResult<MessageResult>(this.warehouseService.MoveAllPalletsToUpper());
        }

        public IResult<MessageResult> MovePalletToUpper(int palletNumber, string reference)
        {
            return new SuccessResult<MessageResult>(
                this.warehouseService.MovePalletToUpper(palletNumber, reference));
        }
    }
}
