namespace Linn.Stores.Domain.LinnApps
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class GoodsInService : IGoodsInService
    {
        private readonly IGoodsInPack goodsInPack;

        private readonly IStoresPack storesPack;

        private readonly IRepository<Part, int> partsRepository;

        public GoodsInService(
            IGoodsInPack goodsInPack,
            IStoresPack storesPack,
            IRepository<Part, int> partsRepository)
        {
            this.storesPack = storesPack;
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
            int? loanNumber,
            int? loanLine,
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
                loanNumber,
                loanLine,
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

        public ValidatePurchaseOrderResult ValidatePurchaseOrder(
            int orderNumber, 
            int line = 1)
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

            var part = this.partsRepository.FindBy(
                x => x.PartNumber.Equals(partNumber.ToUpper()) 
                      && x.QcOnReceipt.Equals("Y"));

            if (!string.IsNullOrEmpty(part?.QcInformation))
            {
                result.PartQcWarning = part.QcInformation;
            }

            var partHasStorageType = this.goodsInPack.PartHasStorageType(
                                         partNumber,
                                         out _,
                                         out var kardex,
                                         out var newPart);

            if (!partHasStorageType)
            {
                if (newPart)
                {
                    result.BookInMessage = "New part - enter storage type or location";
                }

                result.Storage = "BB";
            }
            else
            {
                result.Storage = kardex;
                result.BookInMessage = message;
            }

            result.OrderNumber = orderNumber;
            result.OrderLine = line;
            result.TransactionType = "O";
            result.QtyBookedIn = this.storesPack.GetQuantityBookedIn(orderNumber, line);

            result.ManufacturersPartNumber = manufacturerPartNumber;
            result.PartNumber = partNumber;
            result.PartDescription = description;
            result.OrderUnitOfMeasure = uom;
            result.OrderQty = orderQty;
            result.QcPart = qualityControlPart;
            result.DocumentType = docType;
            result.State = !string.IsNullOrEmpty(part?.QcOnReceipt) 
                                    && part.QcOnReceipt.Equals("Y") ? "QC" : "STORES";

            return result;
        }
    }
}
