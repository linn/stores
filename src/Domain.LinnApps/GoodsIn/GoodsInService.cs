namespace Linn.Stores.Domain.LinnApps.GoodsIn
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Domain.LinnApps.RemoteServices;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Domain.LinnApps.Requisitions;

    public class GoodsInService : IGoodsInService
    {
        private readonly IGoodsInPack goodsInPack;

        private readonly IStoresPack storesPack;

        private readonly IPalletAnalysisPack palletAnalysisPack;

        private readonly IRepository<Part, int> partsRepository;

        private readonly IRepository<GoodsInLogEntry, int> goodsInLog;

        private readonly IRepository<RequisitionHeader, int> reqRepository;

        private readonly IPurchaseOrderPack purchaseOrderPack;

        private readonly IQueryRepository<StoresLabelType> labelTypeRepository;

        private readonly IBartenderLabelPack bartender;

        private readonly IRepository<PurchaseOrder, int> purchaseOrderRepository;

        private readonly IQueryRepository<AuthUser> authUserRepository;

        public GoodsInService(
            IGoodsInPack goodsInPack,
            IStoresPack storesPack,
            IPalletAnalysisPack palletAnalysisPack,
            IRepository<Part, int> partsRepository,
            IRepository<GoodsInLogEntry, int> goodsInLog,
            IRepository<RequisitionHeader, int> reqRepository,
            IPurchaseOrderPack purchaseOrderPack,
            IQueryRepository<StoresLabelType> labelTypeRepository,
            IBartenderLabelPack bartender,
            IRepository<PurchaseOrder, int> purchaseOrderRepository,
            IQueryRepository<AuthUser> authUserRepository)
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
            IEnumerable<GoodsInLogEntry> lines)
        {
            if (string.IsNullOrEmpty(ontoLocation))
            {
                return new BookInResult(false, "Onto location/pallet must be entered");
            }

            if (ontoLocation.StartsWith("P") 
                && !string.IsNullOrEmpty(partNumber))
            {
                if (!this.palletAnalysisPack.CanPutPartOnPallet(partNumber, ontoLocation.TrimStart('P')))
                {
                    return new BookInResult(false, this.palletAnalysisPack.Message());
                }
            }

            var goodsInLogEntries = lines as GoodsInLogEntry[] ?? lines.ToArray();
            var bookinRef = this.goodsInPack.GetNextBookInRef();

            if (!goodsInLogEntries.Any())
            {
                return new BookInResult(false, "Nothing to book in");
            }

            foreach (var goodsInLogEntry in goodsInLogEntries)
            {
                goodsInLogEntry.Id = this.goodsInPack.GetNextLogId();
                goodsInLogEntry.BookInRef = bookinRef;
                goodsInLogEntry.StoragePlace = ontoLocation;
                this.goodsInLog.Add(goodsInLogEntry);
            }

            var message = this.goodsInPack.DoBookIn(
                bookinRef,
                transactionType,
                createdBy,
                partNumber,
                qty,
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

            if (success)
            {
                result.Lines = goodsInLogEntries;
            }

            if (!reqNumberResult.HasValue)
            {
                return result;
            }

            var req = this.reqRepository.FindById((int)reqNumberResult);
            result.ReqNumber = req.ReqNumber;
            var reqLine = req.Lines?.FirstOrDefault();
            result.DocType = reqLine?.TransactionDefinition?.DocType;

            result.QcState = !string.IsNullOrEmpty(state) && state.Equals("QC") ? "QUARANTINE" : "PASS";
            
            result.TransactionCode = reqLine?.TransactionCode;
            
            var part = this.partsRepository.FindBy(x => x.PartNumber.Equals(partNumber.ToUpper()));
            
            result.QcInfo = part?.QcInformation;
            
            if (!string.IsNullOrEmpty(storageType))
            {
                result.KardexLocation = ontoLocation;
            }
            
            if (orderNumber.HasValue)
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
            }

            if (transactionType.Equals("L"))
            {
                result.DocType = "L";
            }

            result.QtyReceived = qty;
            result.PartNumber = partNumber;
            result.PartDescription = part.Description;
            
            if ((multipleBookIn != null && multipleBookIn.Value) 
                || !new[] { "L", "O", "R" }.Contains(transactionType) 
                || !this.goodsInPack.ParcelRequired(
                    orderNumber,
                    rsnNumber,
                    loanNumber,
                    out _))
            {
                return result;
            }
            
            result.CreateParcel = this.goodsInPack.ParcelRequired(
                orderNumber,
                rsnNumber,
                loanNumber,
                out var supplierId);
            
            if (orderNumber.HasValue)
            {
                result.ParcelComments = $"{result.DocType}{orderNumber}";
            }
            
            result.SupplierId = supplierId;
            result.CreatedBy = createdBy;
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

            if (docType != "PO")
            {
                throw new NotImplementedException("Printing for this document type not yet implemented.");
            }

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
                           Message = this.goodsInPack.GetErrorMessage()
                       };
        }
    }
}
