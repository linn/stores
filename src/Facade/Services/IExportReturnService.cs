namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public interface IExportReturnService : IFacadeService<ExportReturn, int, ExportReturnResource, ExportReturnResource>
    {
        IResult<IEnumerable<ExportRsn>> SearchRsns(int accountId, int? outletNumber);

        IResult<ExportReturn> MakeExportReturn(IEnumerable<int> rsns, bool hubReturn);

        IResult<ExportReturn> UpdateExportReturn(int id, ExportReturnResource resource);

        void MakeIntercompanyInvoices(int returnId);
    }
}