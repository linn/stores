namespace Linn.Stores.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;

    public interface ITqmsReportsFacadeService
    {
        IResult<ResultsModel> GetTqmsSummaryByCategory(string jobRef);
    }
}
