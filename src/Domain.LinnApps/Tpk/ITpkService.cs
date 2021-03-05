namespace Linn.Stores.Domain.LinnApps.Tpk
{
    using System.Collections.Generic;

    using Linn.Stores.Domain.LinnApps.Tpk.Models;

    public interface ITpkService
    {
        TpkResult TransferStock(IEnumerable<TransferableStock> stockToTransfer);
    }
}
