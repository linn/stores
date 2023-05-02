namespace Linn.Stores.Domain.LinnApps.Reports
{
    using System;

    using Linn.Common.Reporting.Models;

    public interface IStoresMoveLogReportService
    {
        ResultsModel GetReport(DateTime fromDate, DateTime toDate, string partNumber, string transType, string location, string stockPool);
    }
}
