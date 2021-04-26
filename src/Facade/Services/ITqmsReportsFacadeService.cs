namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;

    public interface ITqmsReportsFacadeService
    {
        IResult<IEnumerable<ResultsModel>> GetTqmsSummaryByCategory(string jobRef);
    }
}
