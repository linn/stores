namespace Linn.Stores.Domain.LinnApps.StockMove
{
    using System;

    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.StockMove.Models;

    public class MoveStockService : IMoveStockService
    {
        private readonly IStoresPack storesPack;

        public MoveStockService(IStoresPack storesPack)
        {
            this.storesPack = storesPack;
        }

        public RequisitionProcessResult MoveStock(
            int? reqNumber,
            string partNumber,
            int quantity,
            string @from,
            int? fromLocationId,
            int? fromPalletNumber,
            DateTime? fromStockRotationDate,
            string to,
            int? toLocationId,
            int? toPalletNumber,
            DateTime? toStockRotationDate,
            int userNumber)
        {
            var result = new RequisitionProcessResult();
            if (!reqNumber.HasValue)
            {
                var moveResult = this.storesPack.CreateMoveReq(userNumber);
                if (!moveResult.Success)
                {
                    throw new CreateReqFailureException("Failed to create move req");
                }

                result.ReqNumber = moveResult.ReqNumber;
            }

            return result;
        }
    }
}
