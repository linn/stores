namespace Linn.Stores.Domain.LinnApps
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class GoodsInService : IGoodsInService
    {
        private readonly IGoodsInPack goodsInPack;

        private readonly IRepository<Part, int> partsRepository;

        public GoodsInService(IGoodsInPack goodsInPack, IRepository<Part, int> partsRepository)
        {
            this.goodsInPack = goodsInPack;
            this.partsRepository = partsRepository;
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

        public ValidatePurchaseOrderResult ValidatePurchaseOrder(int orderNumber, int line = 1)
        {
            var result = new ValidatePurchaseOrderResult
                             {
                                 OrderNumber = orderNumber,
                                 OrderLine = line
                             };

            this.goodsInPack.GetPurchaseOrderDetails(
                orderNumber,
                line,
                out var partNumber,
                out var description,
                out var uom,
                out var orderQty,
                out var qualityControlPart,
                out var manufacturerPartNumber,
                out var docType,
                out var message);

            if (!string.IsNullOrEmpty(message))
            {
                result.OrderNumber = null;
                result.OrderLine = null;
            }

            var part = this.partsRepository.FindBy(
                x => x.PartNumber.Equals(partNumber.ToUpper()) && x.QcOnReceipt.Equals("Y"));

            if (string.IsNullOrEmpty(part.QcInformation))
            {

            }

            return result;
        }
    }
}
