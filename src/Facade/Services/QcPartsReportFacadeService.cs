namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Common.Reporting.Resources.Extensions;
    using Linn.Stores.Domain.LinnApps.Reports;

    public class QcPartsReportFacadeService : IQcPartsReportFacadeService
    {
        private readonly IQcPartsReportService domainService;

        public QcPartsReportFacadeService(IQcPartsReportService domainService)
        {
            this.domainService = domainService;
        }

        public IResult<ResultsModel> GetReport()
        {
            return new SuccessResult<ResultsModel>(this.domainService.GetReport());
        }

        public IResult<IEnumerable<IEnumerable<string>>> GetExport()
        {
            return new SuccessResult<IEnumerable<IEnumerable<string>>>(
                this.domainService.GetReport().ConvertToCsvList());
        }
    }
}
