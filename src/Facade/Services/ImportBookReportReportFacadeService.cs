namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Resources.ImportBooks;

    public class ImportBookReportReportFacadeService : IImportBookReportFacadeService
        
    {
        private readonly IImportBookReportService importBookService;

        public ImportBookReportReportFacadeService(
            IRepository<ImportBook, int> repository,
            IImportBookReportService importBookService)
        {
            this.importBookService = importBookService;
        }

        public IResult<IEnumerable<ImportBook>> GetImpbookIPRReport(IPRSearchResource resource)
        {
            DateTime from;
            DateTime to;
            try
            {
                from = DateTime.Parse(resource.fromDate);
                to = DateTime.Parse(resource.toDate);
            }
            catch (Exception)
            {
                return new BadRequestResult<ResultsModel>("Invalid dates supplied to assembly fails details report");
            }

            return new SuccessResult<ResultsModel>(
                this.reportService.GetAssemblyFailsDetailsReport(
                    from,
                    to,
                    resource.BoardPartNumber,
                    resource.CircuitPartNumber,
                    resource.FaultCode,
                    resource.CitCode,
                    resource.Board,
                    resource.Person));
        }
    }
}
