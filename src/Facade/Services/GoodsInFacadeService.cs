namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Resources.GoodsIn;

    public class GoodsInFacadeService : IGoodsInFacadeService
    {
        private readonly IGoodsInService domainService;

        private readonly IQueryRepository<LoanDetail> loanDetailRepository;

        private readonly IPrintRsnService printRsnService;

        public GoodsInFacadeService(
            IGoodsInService domainService,
            IQueryRepository<LoanDetail> loanDetailRepository,
            IPrintRsnService printRsnService)
        {
            this.domainService = domainService;
            this.loanDetailRepository = loanDetailRepository;
            this.printRsnService = printRsnService;
        }

        public IResult<BookInResult> DoBookIn(BookInRequestResource requestResource)
        {
            requestResource.OntoLocation = requestResource.OntoLocation.ToUpper();
            var lines = requestResource.Lines?.Select(
                l => new GoodsInLogEntry
                         {
                             LoanNumber = l.LoanNumber,
                             OrderLine = l.OrderLine,
                             ArticleNumber = l.ArticleNumber,
                             BookInRef = l.BookInRef,
                             Comments = l.Comments,
                             CreatedBy = l.CreatedBy,
                             DateCreated = DateTime.Parse(l.DateCreated),
                             DemLocation = l.DemLocation,
                             ManufacturersPartNumber = l.ManufacturersPartNumber,
                             State = l.State,
                             OrderNumber = l.OrderNumber,
                             SerialNumber = l.SerialNumber,
                             LoanLine = l.LoanLine,
                             LogCondition = l.LogCondition,
                             Quantity = l.Quantity,
                             RsnAccessories = l.RsnAccessories,
                             RsnNumber = l.RsnNumber,
                             StoragePlace = l.Location,
                             StorageType = l.StorageType,
                             TransactionType = l.TransactionType,
                             WandString = l.WandString
                         });
            var result = this.domainService.DoBookIn(
                requestResource.TransactionType,
                requestResource.CreatedBy,
                requestResource.PartNumber,
                requestResource.ManufacturersPartNumber,
                requestResource.Qty,
                requestResource.OrderNumber,
                requestResource.OrderLine ?? 1,
                requestResource.LoanNumber,
                requestResource.LoanLine,
                requestResource.RsnNumber,
                requestResource.StorageType,
                requestResource.DemLocation,
                requestResource.OntoLocation,
                requestResource.State,
                requestResource.Comments,
                requestResource.Condition,
                requestResource.RsnAccessories,
                requestResource.ReqNumber,
                requestResource.NumberOfLines,
                requestResource.MultipleBookIn,
                requestResource.PrintRsnLabels ?? false,
                lines);

            return new SuccessResult<BookInResult>(result);
        }

        public IResult<IEnumerable<LoanDetail>> GetLoanDetails(int loanNumber)
        {
            var res = this.loanDetailRepository.FilterBy(x => x.LoanNumber == loanNumber);
            return new SuccessResult<IEnumerable<LoanDetail>>(res);
        }

        public IResult<ValidatePurchaseOrderResult> ValidatePurchaseOrder(int orderNumber, int line)
        {
            var result = this.domainService.ValidatePurchaseOrder(orderNumber, line);
            return new SuccessResult<ValidatePurchaseOrderResult>(
                this.domainService.ValidatePurchaseOrder(orderNumber, line));
        }

        public IResult<ProcessResult> ValidatePurchaseOrderQty(int orderNumber, int orderLine, int qty)
        {
            return new SuccessResult<ProcessResult>(this.domainService.ValidatePurchaseOrderQty(
                orderNumber,
                qty,
                orderLine));
        }

        public IResult<ProcessResult> PrintGoodsInLabels(PrintGoodsInLabelsRequestResource requestResource)
        {
            return new SuccessResult<ProcessResult>(this.domainService
                .PrintLabels(
                    requestResource.DocumentType,
                    requestResource.PartNumber, 
                    requestResource.DeliveryRef, 
                    requestResource.Qty, 
                    requestResource.UserNumber,
                    requestResource.OrderNumber,
                    requestResource.NumberOfLines,
                    requestResource.QcState,
                    requestResource.ReqNumber,
                    requestResource.KardexLocation,
                    requestResource.Lines.Select(
                        x => 
                        new GoodsInLabelLine
                            {
                                Id = x.Id,
                                Qty = x.Qty
                            })));
        }

        public IResult<ValidateStorageTypeResult> ValidateStorageType(ValidateStorageTypeRequestResource requestResource)
        {
            return new SuccessResult<ValidateStorageTypeResult>(this.domainService.ValidateStorageType(
                null,
                null,
                null,
                requestResource.StorageType,
                null));
        }

        public IResult<ValidateRsnResult> ValidateRsn(int rsnNumber)
        {
            return new SuccessResult<ValidateRsnResult>(this.domainService.ValidateRsn(rsnNumber));
        }

        public IResult<ProcessResult> PrintRsn(int rsnNumber, int userNumber)
        {
            this.printRsnService.PrintRsn(rsnNumber, userNumber, "Service Copy");

            return new SuccessResult<ProcessResult>(new ProcessResult(true, "Printing..."));
        }
    }
}
