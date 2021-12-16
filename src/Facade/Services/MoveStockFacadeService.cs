namespace Linn.Stores.Facade.Services
{
    using System;

    using Linn.Common.Domain.Exceptions;
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockMove;
    using Linn.Stores.Domain.LinnApps.StockMove.Models;
    using Linn.Stores.Resources.RequestResources;

    public class MoveStockFacadeService : IMoveStockFacadeService
    {
        private readonly IMoveStockService moveStockService;

        public MoveStockFacadeService(IMoveStockService moveStockService)
        {
            this.moveStockService = moveStockService;
        }

        public IResult<RequisitionProcessResult> MoveStock(MoveStockRequestResource requestResource)
        {
            var fromDate = string.IsNullOrEmpty(requestResource.FromStockRotationDate)
                               ? (DateTime?)null
                               : DateTime.Parse(requestResource.FromStockRotationDate);
            var toDate = string.IsNullOrEmpty(requestResource.ToStockRotationDate)
                             ? (DateTime?)null
                             : DateTime.Parse(requestResource.ToStockRotationDate);
            try
            {
                var result = this.moveStockService.MoveStock(
                    requestResource.ReqNumber,
                    requestResource.PartNumber?.ToUpper(),
                    requestResource.Quantity,
                    requestResource.From?.ToUpper(),
                    requestResource.FromLocationId,
                    requestResource.FromPalletNumber,
                    fromDate,
                    requestResource.FromState,
                    requestResource.FromStockPoolCode,
                    requestResource.To?.ToUpper(),
                    requestResource.ToLocationId,
                    requestResource.ToPalletNumber,
                    toDate,
                    requestResource.StorageType?.ToUpper(),
                    requestResource.UserNumber);
                if (result.Success)
                {
                    return new SuccessResult<RequisitionProcessResult>(result);
                }

                return new BadRequestResult<RequisitionProcessResult>(result.Message);
            }
            catch (DomainException exception)
            {
                return new BadRequestResult<RequisitionProcessResult>(exception.Message);
            }
        }
    }
}
