namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class TpkService : ITpkService
    {
        private readonly IQueryRepository<TransferableStock> repository;

        public TpkService(IQueryRepository<TransferableStock> repository)
        {
            this.repository = repository;
        }

        public IResult<IEnumerable<TransferableStock>> GetTransferableStock()
        {
            return new SuccessResult<IEnumerable<TransferableStock>>(this.repository.FindAll());
        }
    }
}
