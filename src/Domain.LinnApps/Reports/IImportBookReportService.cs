namespace Linn.Stores.Domain.LinnApps.Reports
{
    using System;

    using Linn.Common.Reporting.Models;

    public interface IImportBookReportService
    {
        ResultsModel GetIPRReport(DateTime from, DateTime to, bool iprResults);

        ResultsModel GetIPRExport(DateTime from, DateTime to, bool iprResults);

        ResultsModel GetEUReport(DateTime from, DateTime to, bool euResults);

        ResultsModel GetEUExport(DateTime from, DateTime to, bool euResults);
    }
}
