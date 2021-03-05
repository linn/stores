namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Tpk;
    using Linn.Stores.Domain.LinnApps.Tpk.Models;
    using Linn.Stores.Resources;

    public class TpkFacadeService : ITpkFacadeService
    {
        private readonly IQueryRepository<TransferableStock> repository;

        public TpkFacadeService(IQueryRepository<TransferableStock> repository)
        {
            this.repository = repository;
        }

        public IResult<IEnumerable<TransferableStock>> GetTransferableStock()
        {
            return new SuccessResult<IEnumerable<TransferableStock>>(this.repository.FindAll());
        }

        public IResult<TpkResult> TransferStock(IEnumerable<TransferableStockResource> toTransfer)
        {
            throw new System.NotImplementedException();
        }
    }
}
