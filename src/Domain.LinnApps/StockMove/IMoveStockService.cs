namespace Linn.Stores.Domain.LinnApps.StockMove
{
    using Linn.Stores.Domain.LinnApps.Models;

    public interface IMoveStockService
    {
        ProcessResult MoveStock(
            int? reqNumber,
            string partNumber,
            int quantity,
            string from,
            int? fromLocationId,
            int? fromPalletNumber,
            string to,
            int? toLocationId,
            int? toPalletNumber);
    }
}
