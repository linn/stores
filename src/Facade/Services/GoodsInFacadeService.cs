namespace Linn.Stores.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Resources.RequestResources;

    public class GoodsInFacadeService : IGoodsInFacadeService
    {
        private readonly IGoodsInService domainService;

        public GoodsInFacadeService(IGoodsInService domainService)
        {
            this.domainService = domainService;
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
    }
}
