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
            var fromValid = DateTime.TryParse(resource.FromDate, out var from);
            var toValid = DateTime.TryParse(resource.ToDate, out var to);

            if (!fromValid || !toValid)
            {
                return new BadRequestResult<ResultsModel>(
                    "Invalid dates supplied to impbook IPR report");
            }

            return new SuccessResult<ResultsModel>(this.reportService.GetIPRReport(from, to, resource.IprResults));
        }

        public IResult<IEnumerable<IEnumerable<string>>> GetImpbookIprReportExport(IPRSearchResource resource)
        {
            var fromValid = DateTime.TryParse(resource.FromDate, out var from);
            var toValid = DateTime.TryParse(resource.ToDate, out var to);

            if (!fromValid || !toValid)
            {
                return new BadRequestResult<IEnumerable<IEnumerable<string>>>(
                    "Invalid dates supplied to impbook IPR report export");
            }

            return new SuccessResult<IEnumerable<IEnumerable<string>>>(
                this.reportService.GetIPRExport(from, to, resource.IprResults).ConvertToCsvList());
        }

        public IResult<ResultsModel> GetImpbookEuReport(EUSearchResource resource)
        {
            var fromValid = DateTime.TryParse(resource.FromDate, out var from);
            var toValid = DateTime.TryParse(resource.ToDate, out var to);

            if (!fromValid || !toValid)
            {
                return new BadRequestResult<ResultsModel>(
                    "Invalid dates supplied to impbook IPR report");
            }

            return new SuccessResult<ResultsModel>(this.reportService.GetEUReport(from, to, resource.EuResults));
        }

        public IResult<IEnumerable<IEnumerable<string>>> GetImpbookEuReportExport(EUSearchResource resource)
        {
            var fromValid = DateTime.TryParse(resource.FromDate, out var from);
            var toValid = DateTime.TryParse(resource.ToDate, out var to);

            if (!fromValid || !toValid)
            {
                return new BadRequestResult<IEnumerable<IEnumerable<string>>>(
                    "Invalid dates supplied to impbook IPR report export");
            }

            return new SuccessResult<IEnumerable<IEnumerable<string>>>(
                this.reportService.GetEUExport(from, to, resource.EuResults).ConvertToCsvList());
        }
    }
}
