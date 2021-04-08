namespace Linn.Stores.Domain.LinnApps.ExternalServices
{
    using System;

    using Linn.Stores.Domain.LinnApps.Models;

    public interface IKardexPack
    {
        ProcessResult MoveStockFromKardex(
            int reqNumber,
            int reqLine,
            string kardexLocation,
            string partNumber,
            int quantity,
            string storageType,
            int? toLocationId,
            int? toPalletNumber);

        ProcessResult MoveStockToKardex(
            int reqNumber,
            int reqLine,
            string kardexLocation,
            string partNumber,
            int quantity,
            DateTime? fromStockDate,
            string storageType,
            int? fromLocationId,
            int? fromPalletNumber,
            DateTime? toStockDate,
            string locationFlag);
    }
}
