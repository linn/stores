namespace Linn.Stores.Domain.LinnApps.Reports
{
    using System.Collections.Generic;

    using Linn.Common.Reporting.Models;

    public interface IStoragePlaceAuditReportService
    {
        ResultsModel StoragePlaceAuditReport(IEnumerable<string> locationList, string locationRange);
    }
}