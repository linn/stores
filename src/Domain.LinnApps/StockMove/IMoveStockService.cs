namespace Linn.Stores.Domain.LinnApps.StockMove
{
    using System;

    using Linn.Stores.Domain.LinnApps.StockMove.Models;

    public interface IMoveStockService
    {
        RequisitionProcessResult MoveStock(
            int? reqNumber,
            string partNumber,
            int quantity,
            string from,
            int? fromLocationId,
            int? fromPalletNumber,
            DateTime? fromStockRotationDate,
            string to,
            int? toLocationId,
            int? toPalletNumber,
            DateTime? toStockRotationDate,
            int userNumber);

        bool IsKardexLocation(string location);

        void GetLocationDetails(string location, out int? locationId, out int? palletNumber);
    }
}
