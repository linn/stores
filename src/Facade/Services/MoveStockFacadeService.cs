namespace Linn.Stores.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Domain.LinnApps.StockMove;
    using Linn.Stores.Resources.RequestResources;

    public class MoveStockFacadeService : IMoveStockFacadeService
    {
        private readonly IMoveStockService moveStockService;

        public MoveStockFacadeService(IMoveStockService moveStockService)
        {
            this.moveStockService = moveStockService;
        }

        public IResult<ProcessResult> MoveStock(MoveStockRequestResource requestResource)
        {
            var result = this.moveStockService.MoveStock(
                requestResource.ReqNumber,
                requestResource.PartNumber,
                requestResource.Quantity,
                requestResource.From,
                requestResource.FromLocationId,
                requestResource.FromPalletNumber,
                requestResource.To,
                requestResource.ToLocationId,
                requestResource.ToPalletNumber);
            if (result.Success)
            {
                return new SuccessResult<ProcessResult>(result);
            }

            return new BadRequestResult<ProcessResult>(result.Message);
        }
    }
}
