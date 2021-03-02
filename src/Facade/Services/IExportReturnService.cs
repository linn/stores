namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;

    public interface IExportReturnService
    {
        IResult<IEnumerable<ExportRsn>> SearchRsns(int accountId, int? outletNumber);

        IResult<ExportReturn> MakeExportReturn(IEnumerable<int> rsns, bool hubReturn);
    }
}