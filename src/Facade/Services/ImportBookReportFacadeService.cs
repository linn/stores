namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Common.Reporting.Resources.Extensions;
    using Linn.Stores.Domain.LinnApps.Reports;
    using Linn.Stores.Resources.ImportBooks;

    public class ImportBookReportFacadeService : IImportBookReportFacadeService
    {
        private readonly IImportBookReportService reportService;

        public ImportBookReportFacadeService(IImportBookReportService reportService)
        {
            this.reportService = reportService;
        }

        public IResult<ResultsModel> GetImpbookIPRReport(IPRSearchResource resource)
        {
            DateTime.TryParse(resource.FromDate, out var from);
            DateTime.TryParse(resource.ToDate, out var to);

            return new SuccessResult<ResultsModel>(this.reportService.GetIPRReport(from, to));
        }

        public IResult<IEnumerable<IEnumerable<string>>> GetImpbookIprReportExport(IPRSearchResource resource)
        {
            DateTime.TryParse(resource.FromDate, out var from);
            DateTime.TryParse(resource.ToDate, out var to);

            return new SuccessResult<IEnumerable<IEnumerable<string>>>(
                this.reportService.GetIPRReport(from, to).ConvertToCsvList());
        }
    }
}
