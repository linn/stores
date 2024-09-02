namespace Linn.Stores.Domain.LinnApps.Reports
{
    using Linn.Common.Reporting.Models;

    public interface IStockReportService
    {
        ResultsModel GetStockLocatorReport(string siteCode);
    }
}
