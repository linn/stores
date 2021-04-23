namespace Linn.Stores.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Stores.Domain.LinnApps.Tqms;

    public class TqmsReportsFacadeService : ITqmsReportsFacadeService
    {
        private readonly ITqmsReportsService tqmsReportsService;

        public TqmsReportsFacadeService(ITqmsReportsService tqmsReportsService)
        {
            this.tqmsReportsService = tqmsReportsService;
        }

        public IResult<ResultsModel> GetTqmsSummaryByCategory(string jobRef)
        {
            return new SuccessResult<ResultsModel>(
                this.tqmsReportsService.TqmsSummaryByCategoryReport(jobRef));
        }
    }
}
