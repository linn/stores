namespace Linn.Stores.Domain.LinnApps.Tpk
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.Tpk.Models;

    public class TpkService : ITpkService
    {
        private readonly IQueryRepository<TransferableStock> tpkView;

        public TpkService(IQueryRepository<TransferableStock> tpkView)
        {
            this.tpkView = tpkView;
        }

        public TpkResult TransferStock(IEnumerable<TransferableStock> stockToTransfer)
        {
            var candidates = stockToTransfer.ToList();
            var fromLocation = candidates.First().FromLocation;
            if (candidates.Any(s => s.FromLocation != fromLocation))
            {
                throw new TpkException("You can only TPK one pallet at a time");
            }

            return new TpkResult();
        }
    }
}
