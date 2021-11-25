namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Domain.LinnApps.Tpk;
    using Linn.Stores.Domain.LinnApps.Tpk.Models;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Resources.Tpk;

    public class TpkFacadeService : ITpkFacadeService
    {
        private readonly IQueryRepository<TransferableStock> repository;

        private readonly ITpkService domainService;

        private readonly IStoresPack storesPack;

        public TpkFacadeService(
            IQueryRepository<TransferableStock> repository, 
            ITpkService domainService,
            IStoresPack storesPack)
        {
            this.repository = repository;
            this.domainService = domainService;
            this.storesPack = storesPack;
        }

        public IResult<IEnumerable<TransferableStock>> GetTransferableStock()
        {
            return new SuccessResult<IEnumerable<TransferableStock>>(this.repository.FindAll());
        }

        public IResult<TpkResult> TransferStock(TpkRequestResource tpkRequestResource)
        {
            var tpkRequest = new TpkRequest
                                 {
                                     DateTimeTpkViewQueried = tpkRequestResource.DateTimeTpkViewQueried,
                                     StockToTransfer = tpkRequestResource.StockToTransfer
                                         .Select(s => 
                                             new TransferableStock
                                                 {
                                                     FromLocation = s.FromLocation,
                                                     ConsignmentId = s.ConsignmentId,
                                                     Addressee = s.Addressee,
                                                     ArticleNumber = s.ArticleNumber,
                                                     DespatchLocationCode = s.DespatchLocationCode,
                                                     InvoiceDescription = s.InvoiceDescription,
                                                     LocationCode = s.LocationCode,
                                                     LocationId = s.LocationId,
                                                     OrderLine = s.OrderLine,
                                                     OrderNumber = s.OrderNumber,
                                                     PalletNumber = s.PalletNumber,
                                                     Quantity = s.Quantity,
                                                     ReqLine = s.ReqLine,
                                                     ReqNumber = s.ReqNumber,
                                                     StoragePlaceDescription = s.StoragePlaceDescription,
                                                     VaxPallet = s.VaxPallet
                                                 })
                                 };
            try
            {
                var result = this.domainService.TransferStock(tpkRequest);
                return new SuccessResult<TpkResult>(result);
            }
            catch (TpkException ex)
            {
                return new BadRequestResult<TpkResult>(ex.Message);
            }
        }

        public IResult<ProcessResult> UnallocateReq(UnallocateReqRequestResource resource)
        {
            return new SuccessResult<ProcessResult>(this.storesPack.UnAllocateRequisition(resource.ReqNumber, null, resource.UnallocatedBy));
        }

        public IResult<ProcessResult> UnpickStock(UnpickStockRequestResource resource)
        {
            return new SuccessResult<ProcessResult>(
                this.domainService.UnpickStock(
                    resource.ReqNumber,
                    resource.LineNumber,
                    resource.OrderNumber,
                    resource.OrderLine,
                    resource.AmendedBy,
                    resource.PalletNumber,
                    resource.LocationId));
        }
    }
}
