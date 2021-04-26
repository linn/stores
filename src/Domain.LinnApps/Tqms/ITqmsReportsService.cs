namespace Linn.Stores.Domain.LinnApps.Tqms
{
    using System.Collections.Generic;

    using Linn.Common.Reporting.Models;

    public interface ITqmsReportsService
    {
        IEnumerable<ResultsModel> TqmsSummaryByCategoryReport(string jobRef);
    }
}
