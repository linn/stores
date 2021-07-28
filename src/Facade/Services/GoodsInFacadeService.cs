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

        public GoodsInFacadeService(
            IGoodsInService domainService,
            IQueryRepository<LoanDetail> loanDetailRepository)
        {
            this.domainService = domainService;
            this.loanDetailRepository = loanDetailRepository;
        }

        public IResult<ProcessResult> DoBookIn(BookInRequestResource requestResource)
        {
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
                             Id = l.Id,
                             ManufacturersPartNumber = l.ManufacturersPartNumber,
                             State = l.State,
                             OrderNumber = l.OrderNumber,
                             SerialNumber = l.SerialNumber,
                             LoanLine = l.LoanLine,
                             LogCondition = l.LogCondition,
                             Quantity = l.Quantity,
                             RsnAccessories = l.RsnAccessories,
                             RsnNumber = l.RsnNumber,
                             StoragePlace = l.StoragePlace,
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
                requestResource.OrderLine,
                requestResource.LoanNumber,
                requestResource.LoanLine,
                requestResource.RsnNumber,
                requestResource.StoragePlace,
                requestResource.StorageType,
                requestResource.OntoLocation,
                requestResource.DemLocation,
                requestResource.State,
                requestResource.Comments,
                requestResource.Condition,
                requestResource.RsnAccessories,
                requestResource.ReqNumber,
                requestResource.NumberOfLines,
                lines);

            if (result.Success)
            {
                return new SuccessResult<ProcessResult>(result);
            }

            return new BadRequestResult<ProcessResult>(result.Message);
        }

        public IResult<IEnumerable<LoanDetail>> GetLoanDetails(int loanNumber)
        {
            var res = this.loanDetailRepository.FilterBy(x => x.LoanNumber == loanNumber);
            return new SuccessResult<IEnumerable<LoanDetail>>(res);
        }

        public IResult<ValidatePurchaseOrderResult> ValidatePurchaseOrder(int orderNumber, int line)
        {
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
    }
}
