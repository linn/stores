namespace Linn.Stores.Domain.LinnApps
{
    using Linn.Stores.Domain.LinnApps.Models;

    public interface IGoodsInService
    {
        ProcessResult DoBookIn(
            string transactionType,
            int createdBy,
            string partNumber,
            int qty,
            int? orderNumber,
            int? orderLine,
            int? loanNumber,
            int? loanLine,
            int? rsnNumber,
            string storagePlace,
            string storageType,
            string demLocation,
            string ontoLocation,
            string state,
            string comments,
            string condition,
            string rsnAccessories,
            int? reqNumber);

        ValidatePurchaseOrderResult ValidatePurchaseOrder(int orderNumber, int line);
    }
}
