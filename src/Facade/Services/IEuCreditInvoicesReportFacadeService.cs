namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;

    public interface IEuCreditInvoicesReportFacadeService
    {
        IResult<ResultsModel> GetReport(string from, string to);

        IResult<IEnumerable<IEnumerable<string>>> GetExport(string from, string to);
    }
}
