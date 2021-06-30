namespace Linn.Stores.Domain.LinnApps
{
    using Linn.Stores.Domain.LinnApps.Models;

    public interface IGoodsInService
    {
        ProcessResult DoBookIn(
            int bookInRef,
            string transactionType,
            int createdBy,
            string partNumber,
            int qty,
            int orderNumber,
            int orderLine,
            int rsnNumber,
            string storagePlace,
            string storageType,
            string demLocation,
            string state,
            string comments,
            string condition,
            string rsnAccessories,
            int reqNumber);
    }
}
