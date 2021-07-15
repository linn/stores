namespace Linn.Stores.Facade.Services
{
    using System;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Domain.LinnApps.Reports;
    using Linn.Stores.Resources.ImportBooks;

    public class ImportBookReportReportFacadeService : IImportBookReportFacadeService
    {
        private readonly IImportBookReportService reportService;

        public ImportBookReportReportFacadeService(
            IRepository<ImportBook, int> repository,
            IImportBookReportService reportService)
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
                return new BadRequestResult<ResultsModel>("Invalid dates supplied to assembly fails details report");
            }

            return new SuccessResult<ResultsModel>(this.reportService.GetIPRReport(from, to));
        }
    }
}
