namespace Linn.Stores.Domain.LinnApps.StockMove
{
    using System;

    using Linn.Stores.Domain.LinnApps.StockMove.Models;

    public interface IMoveStockService
    {
        RequisitionProcessResult MoveStock(
            int? reqNumber,
            string partNumber,
            decimal quantity,
            string from,
            int? fromLocationId,
            int? fromPalletNumber,
            DateTime? fromStockRotationDate,
            string fromState,
            string fromStockPool,
            string to,
            int? toLocationId,
            int? toPalletNumber,
            DateTime? toStockRotationDate,
            string storageType,
            int userNumber);

        bool IsKardexLocation(string location);

        void GetLocationDetails(string location, out int? locationId, out int? palletNumber);
    }
}
