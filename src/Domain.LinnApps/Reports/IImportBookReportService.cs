namespace Linn.Stores.Domain.LinnApps.Reports
{
    using System;

    using Linn.Common.Reporting.Models;

    public interface IImportBookReportService
    {
        ResultsModel GetIPRReport(DateTime from, DateTime to);
    }
}
