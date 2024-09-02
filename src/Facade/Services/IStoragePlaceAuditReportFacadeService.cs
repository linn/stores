namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;

    public interface IStoragePlaceAuditReportFacadeService
    {
        IResult<ResultsModel> GetStoragePlaceAuditReport(IEnumerable<string> locationList, string locationRange);

        IResult<IEnumerable<IEnumerable<string>>> GetStoragePlaceAuditReportExport(IEnumerable<string> locationList, string locationRange);

    }
}