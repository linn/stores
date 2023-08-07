namespace Linn.Stores.Domain.LinnApps.Reports
{
    using Linn.Common.Reporting.Models;

    public interface IStockTriggerLevelsForAStoragePlaceReportService
    {
        ResultsModel GetReport(string location);
    }
}
