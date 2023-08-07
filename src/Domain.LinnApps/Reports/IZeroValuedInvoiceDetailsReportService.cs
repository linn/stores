namespace Linn.Stores.Domain.LinnApps.Reports
{
    using System;

    using Linn.Common.Reporting.Models;

    public interface IZeroValuedInvoiceDetailsReportService
    {
        ResultsModel GetZeroValuedInvoicedItemsBetweenDates(DateTime from, DateTime to);
    }
}
