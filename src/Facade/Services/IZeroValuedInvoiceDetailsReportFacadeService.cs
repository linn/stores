namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;

    public interface IZeroValuedInvoiceDetailsReportFacadeService
    {
        IResult<IEnumerable<IEnumerable<string>>> GetCsvReport(string fromDate, string toDate);
    }
}
