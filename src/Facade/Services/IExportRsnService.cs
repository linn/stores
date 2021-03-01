namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Models;

    public interface IExportRsnService
    {
        IResult<IEnumerable<ExportRsn>> SearchRsns(int accountId, int? outletNumber);

        IResult<MakeExportReturnResult> MakeExportReturn(IEnumerable<int> rsns, bool hubReturn);
    }
}