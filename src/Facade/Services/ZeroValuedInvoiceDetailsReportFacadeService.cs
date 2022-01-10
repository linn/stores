namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Resources.Extensions;
    using Linn.Stores.Domain.LinnApps.Reports;

    public class ZeroValuedInvoiceDetailsReportFacadeService 
        : IZeroValuedInvoiceDetailsReportFacadeService
    {
        private readonly IZeroValuedInvoiceDetailsReportService reportService;

        public ZeroValuedInvoiceDetailsReportFacadeService(
            IZeroValuedInvoiceDetailsReportService reportService)
        {
            this.reportService = reportService;
        }

        public IResult<IEnumerable<IEnumerable<string>>> GetCsvReport(string fromDate, string toDate)
        {
            var result = this.reportService.GetZeroValuedInvoicedItemsBetweenDates(
                DateTime.Parse(fromDate),
                DateTime.Parse(toDate)).ConvertToCsvList();
            return new SuccessResult<IEnumerable<IEnumerable<string>>>(result);
        }
    }
}
