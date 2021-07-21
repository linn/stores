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
            DateTime from;
            DateTime to;
            try
            {
                from = DateTime.Parse(resource.FromDate);
                to = DateTime.Parse(resource.ToDate);
            }
            catch (Exception)
            {
                return new BadRequestResult<ResultsModel>("Invalid dates supplied to impbook IPR report");
            }

            return new SuccessResult<ResultsModel>(this.reportService.GetIPRReport(from, to));
        }

        public IResult<IEnumerable<IEnumerable<string>>> GetImpbookIprReportExport(IPRSearchResource resource)
        {
            DateTime from;
            DateTime to;
            try
            {
                from = DateTime.Parse(resource.FromDate);
                to = DateTime.Parse(resource.ToDate);
            }
            catch (Exception)
            {
                return new BadRequestResult<IEnumerable<IEnumerable<string>>>(
                    "Invalid dates supplied to impbook IPR report export");
            }

            return new SuccessResult<IEnumerable<IEnumerable<string>>>(
                this.reportService.GetIPRReport(from, to).ConvertToCsvList());
        }
    }
}
