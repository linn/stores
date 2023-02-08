namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;

    public interface IStockTriggerLevelsForAStoragePlaceFacadeService
    {
        IResult<ResultsModel> GetReport(string place);

        IResult<IEnumerable<IEnumerable<string>>> GetExport(string place);
    }
}
