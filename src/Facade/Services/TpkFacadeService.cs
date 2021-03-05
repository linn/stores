namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Tpk;
    using Linn.Stores.Domain.LinnApps.Tpk.Models;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.Tpk;

    public class TpkFacadeService : ITpkFacadeService
    {
        private readonly IQueryRepository<TransferableStock> repository;

        private readonly ITpkService domainService;

        public TpkFacadeService(IQueryRepository<TransferableStock> repository, ITpkService domainService)
        {
            this.repository = repository;
            this.domainService = domainService;
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
                                         .Select(s => new TransferableStock
                                                          {
                                                              FromLocation = s.FromLocation,
                                                              ConsignmentId = s.ConsignmentId,
                                                          })
                                 };
            return new SuccessResult<TpkResult>(
                this.domainService
                    .TransferStock(tpkRequest));
        }
    }
}
