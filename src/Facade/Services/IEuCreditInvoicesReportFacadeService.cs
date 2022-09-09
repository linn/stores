namespace Linn.Stores.Facade.Services
{
    using System;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;

    public interface IEuCreditInvoicesReportFacadeService
    {
        IResult<ResultsModel> GetReport(string from, string to);
    }
}
