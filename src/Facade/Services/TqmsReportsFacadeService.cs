namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

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

        public IResult<IEnumerable<ResultsModel>> GetTqmsSummaryByCategory(string jobRef)
        {
            return new SuccessResult<IEnumerable<ResultsModel>>(
                this.tqmsReportsService.TqmsSummaryByCategoryReport(jobRef));
        }
    }
}
