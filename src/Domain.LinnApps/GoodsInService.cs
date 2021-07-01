namespace Linn.Stores.Domain.LinnApps
{
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Models;

    public class GoodsInService : IGoodsInService
    {
        private readonly IGoodsInPack goodsInPack;

        public GoodsInService(IGoodsInPack goodsInPack)
        {
            this.goodsInPack = goodsInPack;
        }

        public ProcessResult DoBookIn(
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
            int reqNumber)
        {
            this.goodsInPack.DoBookIn(
                bookInRef,
                transactionType,
                createdBy,
                partNumber,
                qty,
                orderNumber,
                orderLine,
                rsnNumber,
                storagePlace,
                storageType,
                demLocation,
                state,
                comments,
                condition,
                rsnAccessories,
                reqNumber,
                out var success);

            return new ProcessResult(
                success, 
                success ? null : this.goodsInPack.GetErrorMessage());
        }

        public ValidatePurchaseOrderResult ValidatePurchaseOrder(int orderNumber)
        {
            throw new System.NotImplementedException();
        }
    }
}
