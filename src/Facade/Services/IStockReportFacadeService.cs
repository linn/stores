namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Stores.Resources.RequestResources;

    public interface IStockReportFacadeService
    {
        IResult<ResultsModel> GetStockLocatorReport(StockLocatorReportRequestResource request);

        IResult<IEnumerable<IEnumerable<string>>> GetStockLocatorReportExport(StockLocatorReportRequestResource request);
    }
}
