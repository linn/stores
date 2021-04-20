﻿namespace Linn.Stores.Domain.LinnApps.ExternalServices
{
    using System;

    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Domain.LinnApps.StockMove.Models;

    public interface IStoresPack
    {
        ProcessResult UnAllocateRequisition(int reqNumber, int? reqLineNumber, int userNumber);

        RequisitionProcessResult CreateMoveReq(int userNumber);

        RequisitionProcessResult CheckStockAtFromLocation(
            string partNumber,
            int quantity,
            string from,
            int? fromLocationId,
            int? fromPalletNumber,
            DateTime? fromStockDate);

        ProcessResult MoveStock(
            int reqNumber,
            int reqLine,
            string partNumber,
            int quantity,
            int? fromLocationId,
            int? fromPalletNumber,
            DateTime? fromStockDate,
            int? toLocationId,
            int? toPalletNumber,
            DateTime? toStockDate,
            string state,
            string stockPool);
    }
}
