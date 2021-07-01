namespace Linn.Stores.Domain.LinnApps.ExternalServices
{
    public interface IGoodsInPack
    {
        void DoBookIn(
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
            out bool success);

        string GetErrorMessage();

        void GetPurchaseOrderDetails(
            int orderNumber,
            int orderLine,
            out string partNumber,
            out string description,
            out string uom,
            out int orderQty,
            out string qcPart,
            out string manufPartNumber,
            out string docType,
            out string errorMess);

        bool PartHasStorageType(string partNumber, out int bookInLoc, out string kardex, out bool newPart);
    }
}
