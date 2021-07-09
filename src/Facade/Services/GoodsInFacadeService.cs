namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Resources.RequestResources;

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
            var result = this.domainService.DoBookIn(
                requestResource.BookInRef,
                requestResource.TransactionType,
                requestResource.CreatedBy,
                requestResource.PartNumber,
                requestResource.Qty,
                requestResource.OrderNumber,
                requestResource.OrderLine,
                requestResource.LoanNumber,
                requestResource.LoanLine,
                requestResource.RsnNumber,
                requestResource.StoragePlace,
                requestResource.StorageType,
                requestResource.DemLocation,
                requestResource.State,
                requestResource.Comments,
                requestResource.Condition,
                requestResource.RsnAccessories,
                requestResource.ReqNumber);

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
    }
}
