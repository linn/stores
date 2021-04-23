namespace Linn.Stores.Domain.LinnApps.Tqms
{
    using Linn.Common.Reporting.Models;

    public interface ITqmsReportsService
    {
        ResultsModel TqmsSummaryByCategoryReport(string jobRef);
    }
}
