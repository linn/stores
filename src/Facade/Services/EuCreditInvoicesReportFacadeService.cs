namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Common.Reporting.Resources.Extensions;
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

        public IResult<IEnumerable<IEnumerable<string>>> GetExport(string from, string to)
        {
            var result = this.domainService.GetReport(DateTime.Parse(from), DateTime.Parse(to));
            return new SuccessResult<IEnumerable<IEnumerable<string>>>(
                result.ConvertToCsvList());
        }
    }
}
