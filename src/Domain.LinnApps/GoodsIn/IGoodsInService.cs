namespace Linn.Stores.Domain.LinnApps.GoodsIn
{
    using System.Collections.Generic;

    using Linn.Stores.Domain.LinnApps.Models;

    public interface IGoodsInService
    {
        ProcessResult DoBookIn(
            string transactionType,
            int createdBy,
            string partNumber,
            string manufacturersPartNumber,
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
            int? reqNumber,
            int? numberOfLines,
            IEnumerable<GoodsInLogEntry> lines);

        ValidatePurchaseOrderResult ValidatePurchaseOrder(int orderNumber, int line);

        ProcessResult ValidatePurchaseOrderQty(
            int orderNumber, 
            int qty,
            int? orderLine = 1);
    }
}
