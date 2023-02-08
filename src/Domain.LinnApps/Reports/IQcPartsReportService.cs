namespace Linn.Stores.Domain.LinnApps.Reports
{
    using Linn.Common.Reporting.Models;

    public interface IQcPartsReportService
    {
        ResultsModel GetReport();
    }
}
