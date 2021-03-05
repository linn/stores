namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Tpk;
    using Linn.Stores.Domain.LinnApps.Tpk.Models;
    using Linn.Stores.Resources;

    public interface ITpkFacadeService
    {
        IResult<IEnumerable<TransferableStock>> GetTransferableStock();

        IResult<TpkResult> TransferStock(IEnumerable<TransferableStockResource> toTransfer);
    }
}
