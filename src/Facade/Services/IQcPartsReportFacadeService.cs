namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;

    public interface IQcPartsReportFacadeService
    {
        IResult<ResultsModel> GetReport();

        IResult<IEnumerable<IEnumerable<string>>> GetExport();
    }
}
