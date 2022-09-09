namespace Linn.Stores.Facade.Services
{
    using System;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Stores.Domain.LinnApps.Reports;

    public class EuCreditInvoicesReportFacadeService : IEuCreditInvoicesReportFacadeService
    {
        private readonly IEuCreditInvoicesReportService domainService;

        public EuCreditInvoicesReportFacadeService(IEuCreditInvoicesReportService domainService)
        {
            this.domainService = domainService;
        }

        public IResult<ResultsModel> GetReport(string from, string to)
        {
            var result = this.domainService.GetReport(DateTime.Parse(from), DateTime.Parse(to));
            return new SuccessResult<ResultsModel>(
                result);
        }
    }
}
