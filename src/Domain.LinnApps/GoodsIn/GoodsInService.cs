namespace Linn.Stores.Domain.LinnApps.GoodsIn
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Domain.LinnApps.RemoteServices;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Exceptions;
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
            IEnumerable<GoodsInLogEntry> lines)
        {
            if (string.IsNullOrEmpty(ontoLocation))
            {
                if ((string.IsNullOrEmpty(storageType) && transactionType == "O") 
                    || transactionType == "L" || transactionType == "D")
                {
                    return new BookInResult(false, "Onto location/pallet must be entered");
                }
            }

            if (ontoLocation != null 
                && ontoLocation.StartsWith("P") 
                && !string.IsNullOrEmpty(partNumber))
            {
                if (!this.palletAnalysisPack.CanPutPartOnPallet(partNumber, ontoLocation.TrimStart('P')))
                {
                    return new BookInResult(false, this.palletAnalysisPack.Message());
                }
            }

            var goodsInLogEntries = lines as GoodsInLogEntry[] ?? lines.ToArray();
            var bookinRef = this.goodsInPack.GetNextBookInRef();

            //// A bookin to just one location. In this case this method won't be called with
            //// an array of 'lines'
            //// so we need to add one entry to the bookin log to reflect this bookin
            if (!goodsInLogEntries.Any())
            {
                return new BookInResult(false, "Nothing to book in");
            }

            // todo - fix front end to always pass lines
            // the request included multiple lines,
            // e.g. booking a PO line into multiple locations or booking in multiple PO's at once
            // so add each to the log under the same bookin ref
            foreach (var goodsInLogEntry in goodsInLogEntries)
            {
                goodsInLogEntry.Id = this.goodsInPack.GetNextLogId();
                goodsInLogEntry.BookInRef = bookinRef;
                this.goodsInLog.Add(goodsInLogEntry);
            }

            // need to make sure changes are committed by this point.
            this.goodsInPack.DoBookIn(
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

            var result = new BookInResult(
                success,
                success ? null : this.goodsInPack.GetErrorMessage());

            if (!reqNumberResult.HasValue)
            {
                return result;
            }

            var req = this.reqRepository.FindById((int)reqNumberResult);
            result.ReqNumber = req.ReqNumber;
            var reqLine = req.Lines?.FirstOrDefault();
            result.DocType = reqLine?.TransactionDefinition?.DocType;

            if (transactionType.Equals("O") && orderNumber.HasValue && partNumber != "PACK 1329")
            {
                result.QcState = !string.IsNullOrEmpty(state) && state.Equals("QC") ? "QUARANTINE" : "PASS";
            }

            result.TransactionCode = reqLine?.TransactionCode;

            var part = this.partsRepository.FindBy(x => x.PartNumber.Equals(partNumber.ToUpper()));

            result.QcInfo = part
                ?.QcInformation;

            // do we need to set kardex location here based on presence of StorageType?

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
            }

            result.QtyReceived = qty;
            result.PartNumber = partNumber;
            result.PartDescription = part.Description;

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
            int numberOfLabels,
            int numberOfLines,
            string qcState,
            int reqNumber,
            IEnumerable<GoodsInLabelLine> lines)
        {
            var labelType = this.labelTypeRepository.FindBy(x => x.Code == qcState);
            var user = this.authUserRepository.FindBy(x => x.UserNumber == userNumber);
            var purchaseOrder = this.purchaseOrderRepository.FindById(orderNumber);
            var part = this.partsRepository.FindBy(x => x.PartNumber == partNumber.ToUpper());

            if (docType != "PO")
            {
                throw new NotImplementedException();
            }

            if (numberOfLines != qty)
            {
                return new ProcessResult(
                    false,
                    $"Quantity Received was {qty}. Quantity Entered is {numberOfLines}.");
            }

            string message = string.Empty;
            bool success = false;

            foreach (var line in lines)
            {
                var printString = string.Empty;

                switch (qcState)
                {
                    case "QUARANTINE":
                        printString += $"\"{docType}{orderNumber}";
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
                        printString += numberOfLabels;
                        printString += "\",\"";
                        printString += line.Qty;
                        printString += "\",\"";
                        printString += line.LineNumber;
                        printString += "\",\"";
                        printString += qcState;
                        printString += "\",\"";
                        printString += "DATE TESTED";
                        printString += "\",\"";
                        printString += reqNumber;
                        printString += "\"";
                        break;
                    case "PASS":
                        var partMessage = purchaseOrder.Details.FirstOrDefault()?.RohsCompliant == "Y"
                                              ? "**ROHS Compliant**"
                                              : null;
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
                        break;
                }

                message = string.Empty;
                success = this.bartender.PrintLabels(
                    $"QC {orderNumber}", 
                    labelType.DefaultPrinter, 
                    1, 
                    labelType.FileName, 
                    printString, 
                    ref message);
            }

            return new ProcessResult(success, message);


            throw new NotImplementedException();
        }
    }
}
