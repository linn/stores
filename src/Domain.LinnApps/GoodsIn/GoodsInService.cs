namespace Linn.Stores.Domain.LinnApps.GoodsIn
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Domain.LinnApps.RemoteServices;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Domain.LinnApps.Requisitions;

    public class GoodsInService : IGoodsInService
    {
        private readonly IGoodsInPack goodsInPack;

        private readonly IStoresPack storesPack;

        private readonly IPalletAnalysisPack palletAnalysisPack;

        private readonly IPartRepository partsRepository;

        private readonly IRepository<GoodsInLogEntry, int> goodsInLog;

        private readonly IRepository<RequisitionHeader, int> reqRepository;

        private readonly IPurchaseOrderPack purchaseOrderPack;

        private readonly IQueryRepository<StoresLabelType> labelTypeRepository;

        private readonly IBartenderLabelPack bartender;

        private readonly IRepository<PurchaseOrder, int> purchaseOrderRepository;

        private readonly IQueryRepository<AuthUser> authUserRepository;

        private readonly IQueryRepository<StoragePlace> storagePlaceRepository;

        private readonly IPrintRsnService printRsnService;

        public GoodsInService(
            IGoodsInPack goodsInPack,
            IStoresPack storesPack,
            IPalletAnalysisPack palletAnalysisPack,
            IPartRepository partsRepository,
            IRepository<GoodsInLogEntry, int> goodsInLog,
            IRepository<RequisitionHeader, int> reqRepository,
            IPurchaseOrderPack purchaseOrderPack,
            IQueryRepository<StoresLabelType> labelTypeRepository,
            IBartenderLabelPack bartender,
            IRepository<PurchaseOrder, int> purchaseOrderRepository,
            IQueryRepository<AuthUser> authUserRepository,
            IPrintRsnService printRsnService,
            IQueryRepository<StoragePlace> storagePlaceRepository)
        {
            this.storesPack = storesPack;
            this.goodsInPack = goodsInPack;
            this.palletAnalysisPack = palletAnalysisPack;
            this.partsRepository = partsRepository;
            this.goodsInLog = goodsInLog;
            this.reqRepository = reqRepository;
            this.purchaseOrderPack = purchaseOrderPack;
            this.labelTypeRepository = labelTypeRepository;
            this.purchaseOrderRepository = purchaseOrderRepository;
            this.bartender = bartender;
            this.authUserRepository = authUserRepository;
            this.storagePlaceRepository = storagePlaceRepository;
            this.printRsnService = printRsnService;
        }

        public BookInResult DoBookIn(
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
            string storageType,
            string demLocation,
            string ontoLocation,
            string state,
            string comments,
            string condition,
            string rsnAccessories,
            int? reqNumber,
            int? numberOfLines,
            bool? multipleBookIn,
            bool printRsnLabels,
            IEnumerable<GoodsInLogEntry> lines)
        {
            if (string.IsNullOrEmpty(ontoLocation))
            {
                return new BookInResult(false, "Onto location/pallet must be entered");
            }

            var linesArray = lines as GoodsInLogEntry[] ?? lines.ToArray();

            if (linesArray.Any(l => this.storagePlaceRepository.FindBy(x => l.StoragePlace == x.Name) == null))
            {
                return new BookInResult(false, "Invalid location entered");
            }

            if (ontoLocation.StartsWith("P") 
                && !string.IsNullOrEmpty(partNumber) && transactionType.Equals("O"))
            {
                foreach (var entry in linesArray)
                {
                    if (!this.palletAnalysisPack.CanPutPartOnPallet(partNumber, entry.StoragePlace.ToUpper().TrimStart('P')))
                    {
                        return new BookInResult(false, $"Can't put {partNumber} on {entry.StoragePlace}");
                    }
                }
                
            }

            var goodsInLogEntries = lines as GoodsInLogEntry[] ?? linesArray.ToArray();
            var bookinRef = this.goodsInPack.GetNextBookInRef();

            if (!goodsInLogEntries.Any())
            {
                return new BookInResult(false, "Nothing to book in");
            }

            foreach (var goodsInLogEntry in goodsInLogEntries)
            {
                goodsInLogEntry.Id = this.goodsInPack.GetNextLogId();
                goodsInLogEntry.BookInRef = bookinRef;
                this.goodsInLog.Add(goodsInLogEntry);
            }

            int? rsnQuantity = null;
            int? serialNumber = null;

            if (transactionType == "R" 
                && !this.goodsInPack.GetRsnDetails((int)rsnNumber, out _, out _, out _, out rsnQuantity, out serialNumber, out var msg))
            {
                return new BookInResult(false, msg);
            }

            var total = linesArray.Sum(x => x.Quantity).Value;

            if (transactionType.Equals("O") && !this.storesPack.ValidOrderQty((int)orderNumber, (int)orderLine, (int)total, out var qtyRec, out _))
            {
                var res = this.ValidatePurchaseOrder((int)orderNumber, (int)orderLine);
                return new BookInResult(false, $"Overbook: PO was for {res.OrderQty} but you have tried to book in {total}");
            }

            var message = this.goodsInPack.DoBookIn(
                bookinRef,
                transactionType,
                createdBy,
                partNumber,
                total,
                orderNumber,
                orderLine,
                loanNumber,
                loanLine,
                rsnNumber,
                ontoLocation,
                storageType,
                demLocation,
                state,
                comments,
                condition,
                rsnAccessories,
                out var reqNumberResult,
                out var success);

            var result = new BookInResult(success, success ? "Book In Successful!" : message);

            if (!success)
            {
                return result;
            }

            result.OrderNumber = orderNumber;

            result.UserNumber = createdBy;

            result.CreateParcel = this.goodsInPack.ParcelRequired(
                                      orderNumber,
                                      rsnNumber,
                                      loanNumber,
                                      out var supplierId) && (multipleBookIn == null || !multipleBookIn.Value);

            var part = this.partsRepository.FindBy(x => x.PartNumber.Equals(partNumber.ToUpper()));

            result.Lines = goodsInLogEntries;

            result.CreatedBy = createdBy;

            result.QcState = !string.IsNullOrEmpty(state) && state.Equals("QC") ? "QUARANTINE" : "PASS";
            result.QcInfo = part?.QcInformation;


            if (transactionType == "O")
            {
                this.goodsInPack.GetPurchaseOrderDetails(
                    orderNumber.Value,
                    orderLine.Value,
                    out _,
                    out _,
                    out var uom,
                    out _,
                    out _,
                    out _,
                    out _,
                    out _);
                result.UnitOfMeasure = uom;
                result.DocType = this.purchaseOrderPack.GetDocumentType(orderNumber.Value);
                result.PrintLabels = true;
                result.PartNumber = partNumber;
                result.PartDescription = part.Description;
                result.SupplierId = supplierId;
                result.ParcelComments = $"{result.DocType}{orderNumber}";

                result.QtyReceived = total;

                if (!string.IsNullOrEmpty(storageType))
                {
                    result.KardexLocation = ontoLocation;
                }

                if (reqNumberResult.HasValue)
                {
                    var req = this.reqRepository.FindById((int)reqNumberResult);
                    result.ReqNumber = req.ReqNumber;
                    var reqLine = req.Lines?.FirstOrDefault();
                    result.DocType = reqLine?.TransactionDefinition?.DocType;

                    result.TransactionCode = reqLine?.TransactionCode;
                }

                return result;
            }

            if (transactionType.Equals("L"))
            {
                result.DocType = "L";
                result.TransactionCode = "L";
                result.QtyReceived = qty;
                result.PartNumber = partNumber;
                result.PartDescription = part.Description;

                result.SupplierId = supplierId;

                return result;
            }

            if (transactionType.Equals("R"))
            {
                if (result.CreateParcel)
                {
                    result.ParcelComments = $"RSN{rsnNumber}";
                }

                this.printRsnService.PrintRsn((int)rsnNumber, createdBy, "Service Copy");

                if (printRsnLabels)
                {
                    this.PrintRsnLabels(
                        (int)rsnNumber,
                        partNumber,
                        serialNumber,
                        rsnQuantity ?? 1);
                    result.TransactionCode = "R";
                }
            }

            return result;
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

            if (!string.IsNullOrEmpty(message))
            {
                result.Message = message;
                return result;
            }

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
                    result.Message = "New part - enter storage type or location";
                }

                result.Storage = "BB";
            }
            else
            {
                result.Storage = kardex;
                result.Message = message;
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

            if (!string.IsNullOrEmpty(uom)
                    && uom.StartsWith("REEL") 
                    && string.IsNullOrEmpty(manufacturerPartNumber))
            {
                result.Message += "\nNo manufacturer part number on part supplier - see Purchasing";
            }

            return result;
        }

        public ProcessResult ValidatePurchaseOrderQty(
            int orderNumber, 
            int qty,
            int? orderLine)
        {
            var valid = this.storesPack.ValidOrderQty(orderNumber, orderLine ?? 1, qty, out _, out _);
            return valid 
                       ? new ProcessResult
                             {
                                 Success = true
                             }
                       : new ProcessResult
                             {
                                 Success = false,
                                 Message = $"Order {orderNumber} is overbooked"
                             };
        }

        public ProcessResult PrintLabels(
            string docType,
            string partNumber,
            string deliveryRef,
            int qty,
            int userNumber,
            int orderNumber,
            int numberOfLines,
            string qcState,
            int reqNumber,
            string kardexLocation,
            IEnumerable<GoodsInLabelLine> lines)
        {
            var message = string.Empty;
            var success = false;

            if (!string.IsNullOrEmpty(kardexLocation))
            {
                var labelName = $"KGI{orderNumber}";
                var data = $"\"{kardexLocation.Replace("\"", "''")}\",\"{reqNumber}\"";
                var kardexLabelType = this.labelTypeRepository.FindBy(x => x.Code == "KARDEX");
                success = this.bartender.PrintLabels(
                    labelName,
                    kardexLabelType.DefaultPrinter,
                    1,
                    kardexLabelType.FileName,
                    data,
                    ref message);

                return new ProcessResult { Message = message, Success = success };
            }

            var labelType = this.labelTypeRepository.FindBy(x => x.Code == qcState);
            var user = this.authUserRepository.FindBy(x => x.UserNumber == userNumber);
            var purchaseOrder = this.purchaseOrderRepository.FindById(orderNumber);
            var part = this.partsRepository.FindBy(x => x.PartNumber == partNumber.ToUpper());

            foreach (var line in lines)
            {
                var printString = string.Empty;

                switch (qcState)
                {
                    case "QUARANTINE":
                        printString += $"\"{docType}{orderNumber}";
                        printString += "\",\"";
                        printString += part.PartNumber;
                        printString += "\",\"";
                        printString += part.Description;
                        printString += "\",\"";
                        printString += deliveryRef;
                        printString += "\",\"";
                        printString += DateTime.Now.ToString("MMMddyyyy").ToUpper();
                        printString += "\",\"";
                        printString += part.OurUnitOfMeasure;
                        printString += "\",\"";
                        printString += user.Initials;
                        printString += "\",\"";
                        printString += DateTime.Now.ToString("MMMddyyyy").ToUpper();
                        printString += "\",\"";
                        printString += string.IsNullOrEmpty(part.QcInformation) ? "NO QC INFO" : part.QcInformation;
                        printString += "\",\"";
                        printString += purchaseOrder.SupplierId;
                        printString += "\",\"";
                        printString += purchaseOrder.Supplier.Name;
                        printString += "\",\"";
                        printString += qty;
                        printString += "\",\"";
                        printString += numberOfLines;
                        printString += "\",\"";
                        printString += line.Qty;
                        printString += "\",\"";
                        printString += line.Id + 1;
                        printString += "\",\"";
                        printString += qcState;
                        printString += "\",\"";
                        printString += "DATE TESTED";
                        printString += "\",\"";
                        printString += reqNumber;
                        printString += "\"";
                        printString += Environment.NewLine;
                        break;
                    case "PASS":
                        var partMessage = purchaseOrder.Details.FirstOrDefault()?.RohsCompliant == "Y"
                                              ? "**ROHS Compliant**"
                                              : null;
                        printString += "\"";
                        printString += orderNumber;
                        printString += "\",\"";
                        printString += partNumber;
                        printString += "\",\"";
                        printString += line.Qty;
                        printString += "\",\"";
                        printString += user.Initials;
                        printString += "\",\"";
                        printString += part.Description;
                        printString += "\",\"";
                        printString += reqNumber;
                        printString += "\",\"";
                        printString += DateTime.Now.ToString("MMMddyyyy").ToUpper();
                        printString += "\",\"";
                        printString += partMessage;
                        printString += "\"";
                        printString += Environment.NewLine;
                        break;
                }

                message = string.Empty;
                success = this.bartender.PrintLabels(
                    $"QC {orderNumber}-{line.Id}", 
                    labelType.DefaultPrinter, 
                    1, 
                    labelType.FileName, 
                    printString, 
                    ref message);
            }

            return new ProcessResult(success, message);
        }

        public ValidateStorageTypeResult ValidateStorageType(
            int? orderNumber,
            string docType,
            string partNumber,
            string storageType,
            int? qty)
        {
            this.goodsInPack.GetKardexLocations(
                orderNumber,
                docType,
                partNumber?.ToUpper(),
                storageType?.ToUpper(),
                out var locationId,
                out var locationCode,
                qty);

            return new ValidateStorageTypeResult
                       {
                           LocationCode = locationCode,
                           LocationId = locationId,
                           Message = string.IsNullOrEmpty(locationCode) ? this.goodsInPack.GetErrorMessage() : null
                       };
        }

        public ProcessResult PrintRsnLabels(int rsnNumber, string partNumber, int? serialNumber, int qty = 1)
        {
            var labelType = this.labelTypeRepository.FindBy(x => x.Code.Equals("RSN_LABEL"));
            var labelName = $"RSN {rsnNumber}";
            string message = null;

            var printString = $"\"{rsnNumber}";
            printString += "\",\"";
            printString += partNumber;
            printString += "\",\"";
            printString += serialNumber;
            printString += "\"";

            var success = this.bartender.PrintLabels(
                labelName,
                labelType.DefaultPrinter,
                qty,
                labelType.FileName,
                printString,
                ref message);
 
            return new ProcessResult(success, message);
        }

        public ValidateRsnResult ValidateRsn(int rsnNumber)
        {
            var success = this.goodsInPack.GetRsnDetails(
                rsnNumber,
                out var state,
                out var articleNumber,
                out var description,
                out var quantity,
                out var serial,
                out var message);

            return new ValidateRsnResult
                       {
                           Success = success,
                           State = state,
                           ArticleNumber = articleNumber,
                           Description = description,
                           Quantity = quantity,
                           SerialNumber = serial,
                           Message = message
                       };
        }
    }
}
