namespace Linn.Stores.Proxy
{
    using Linn.Stores.Domain.LinnApps.ExternalServices;

    public class GoodsInPack : IGoodsInPack
    {
        public void DoBookIn(
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
            int reqNumber,
            out bool success)
        {
            throw new System.NotImplementedException();
        }
    }
}
