namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Tpk;
    using Linn.Stores.Domain.LinnApps.Tpk.Models;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.Tpk;

    public interface ITpkFacadeService
    {
        IResult<IEnumerable<TransferableStock>> GetTransferableStock();

        IResult<TpkResult> TransferStock(TpkRequestResource tpkRequestResource);
    }
}
