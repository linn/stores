namespace Linn.Stores.Domain.LinnApps.Reports
{
    using System;

    using Linn.Common.Reporting.Models;

    public interface IEuCreditInvoicesReportService
    {
        ResultsModel GetReport(DateTime from, DateTime to);
    }
}
