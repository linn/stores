namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Stores.Resources.RequestResources;

    public interface IStoresMoveLogReportFacadeService
    {
        IResult<ResultsModel> GetReport(StoresMoveLogReportRequestResource request);

        IResult<IEnumerable<IEnumerable<string>>> GetExport(StoresMoveLogReportRequestResource request);
    }
}
