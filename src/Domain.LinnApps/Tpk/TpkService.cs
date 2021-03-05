namespace Linn.Stores.Domain.LinnApps.Tpk
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.Tpk.Models;

    public class TpkService : ITpkService
    {
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
