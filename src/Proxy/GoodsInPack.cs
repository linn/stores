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

        public string GetErrorMessage()
        {
            throw new System.NotImplementedException();
        }

        public void GetPurchaseOrderDetails(
            int orderNumber,
            int orderLine,
            out string partNumber,
            out string description,
            out string uom,
            out int orderQty,
            out string qcPart,
            out string manufPartNumber,
            out string docType,
            out string errorMess)
        {
            throw new System.NotImplementedException();
        }

        public bool PartHasStorageType(string partNumber, out int bookInLoc, out string kardex, out bool newPart)
        {
            throw new System.NotImplementedException();
        }
    }
}
