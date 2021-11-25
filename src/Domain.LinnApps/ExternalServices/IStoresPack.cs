namespace Linn.Stores.Domain.LinnApps.ExternalServices
{
    using System;

    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Domain.LinnApps.StockMove.Models;

    public interface IStoresPack
    {
        ProcessResult UnAllocateRequisition(int reqNumber, int? reqLineNumber, int userNumber);

        void DoTpk(int? locationId, int? palletNumber, DateTime dateTimeStarted, out bool success);

        string GetErrorMessage();

        RequisitionProcessResult CreateMoveReq(int userNumber);

        RequisitionProcessResult CheckStockAtFromLocation(
            string partNumber,
            decimal quantity,
            string from,
            int? fromLocationId,
            int? fromPalletNumber,
            DateTime? fromStockDate);

        ProcessResult MoveStock(
            int reqNumber,
            int reqLine,
            string partNumber,
            decimal quantity,
            int? fromLocationId,
            int? fromPalletNumber,
            DateTime? fromStockDate,
            int? toLocationId,
            int? toPalletNumber,
            DateTime? toStockDate,
            string state,
            string stockPool);

        int GetQuantityBookedIn(int purchaseOrderNumber, int line);

        bool ValidOrderQty(int orderNumber, int orderLine, int qty, out int qtyRec, out int ourQty);

        ProcessResult UnallocateReq(int reqNumber, int unallocatedBy);

        ProcessResult UnpickStock(
            int reqNumber, 
            int lineNumber, 
            int seq, 
            int orderNumber, 
            int orderLine, 
            decimal qty, 
            int stockLocatorId, 
            int amendedBy);
    }
}
