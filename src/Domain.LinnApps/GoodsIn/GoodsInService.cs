namespace Linn.Stores.Domain.LinnApps.GoodsIn
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class GoodsInService : IGoodsInService
    {
        private readonly IGoodsInPack goodsInPack;

        private readonly IStoresPack storesPack;

        private readonly IPalletAnalysisPack palletAnalysisPack;

        private readonly IRepository<Part, int> partsRepository;

        public GoodsInService(
            IGoodsInPack goodsInPack,
            IStoresPack storesPack,
            IPalletAnalysisPack palletAnalysisPack,
            IRepository<Part, int> partsRepository)
        {
            this.storesPack = storesPack;
            this.goodsInPack = goodsInPack;
            this.palletAnalysisPack = palletAnalysisPack;
            this.partsRepository = partsRepository;
        }

        public ProcessResult DoBookIn(
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
            int? reqNumber)
        {
            if (string.IsNullOrEmpty(ontoLocation))
            {
                if ((string.IsNullOrEmpty(storageType) && transactionType.Equals("O")) 
                    || transactionType.Equals("L") || transactionType.Equals("D"))
                {
                    return new ProcessResult(false, "Onto location/pallet must be entered");
                }
            }

            if (ontoLocation != null 
                && ontoLocation.StartsWith("P") 
                && !string.IsNullOrEmpty(partNumber))
            {
                if (!this.palletAnalysisPack.CanPutPartOnPallet(partNumber, ontoLocation.TrimStart('P')))
                {
                    return new ProcessResult(false, this.palletAnalysisPack.Message());
                }
            }

            this.goodsInPack.DoBookIn(
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
