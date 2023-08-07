namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Domain.LinnApps.Tpk;
    using Linn.Stores.Domain.LinnApps.Tpk.Models;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Resources.Tpk;

    public interface ITpkFacadeService
    {
        IResult<IEnumerable<TransferableStock>> GetTransferableStock();

        IResult<TpkResult> TransferStock(TpkRequestResource tpkRequestResource);

        IResult<ProcessResult> UnallocateReq(UnallocateReqRequestResource resource);

        IResult<ProcessResult> UnpickStock(UnpickStockRequestResource resource);

        IResult<WhatToWandConsignment> ReprintWhatToWand(int consignmentId);
    }
}
