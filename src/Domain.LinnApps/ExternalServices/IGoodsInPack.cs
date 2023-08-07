namespace Linn.Stores.Domain.LinnApps.ExternalServices
{
    public interface IGoodsInPack
    {
        string DoBookIn(
            int bookInRef,
            string transactionType,
            int createdBy,
            string partNumber,
            decimal qty,
            int? orderNumber,
            int? orderLine,
            int? loanNumber,
            int? loanLine,
            int? rsnNumber,
            string storagePlace,
            string storageType,
            string demLocation,
            string state,
            string comments,
            string condition,
            string rsnAccessories,
            out int? reqNumber,
            out bool success);

        string GetErrorMessage();

        void GetPurchaseOrderDetails(
            int orderNumber,
            int orderLine,
            out string partNumber,
            out string description,
            out string uom,
            out int orderQty,
            out string qualityControlPart,
            out string manufacturerPartNumber,
            out string docType,
            out string message);

        bool PartHasStorageType(string partNumber, out int bookInLocation, out string kardex, out bool newPart);

        int GetNextBookInRef();

        int GetNextLogId();

        void GetKardexLocations(
            int? orderNumber,
            string docType, 
            string partNumber, 
            string storageType, 
            out int? locationId, 
            out string locationCode, 
            int? qty);

        bool ParcelRequired(
            int? orderNumber, 
            int? rsnNumber, 
            int? loanNumber, 
            out int? supplierId);

        bool GetRsnDetails(
            int rsnNumber,
            out string state,
            out string articleNumber,
            out string description,
            out int? quantity,
            out int? serial,
            out string message);
    }
}
