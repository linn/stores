namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Common.Reporting.Resources.Extensions;
    using Linn.Stores.Domain.LinnApps.Reports;
    using Linn.Stores.Resources.RequestResources;

    public class StockReportFacadeService : IStockReportFacadeService
    {
        private readonly IStockReportService stockReportService;

        public StockReportFacadeService(IStockReportService stockReportService)
        {
            this.stockReportService = stockReportService;
        }

        public IResult<ResultsModel> GetStockLocatorReport(StockLocatorReportRequestResource request)
        {
            var result = this.stockReportService.GetStockLocatorReport(request.SiteCode);
            return new SuccessResult<ResultsModel>(result);
        }

        public IResult<IEnumerable<IEnumerable<string>>> GetStockLocatorReportExport(StockLocatorReportRequestResource request)
        {
            var result = this.stockReportService.GetStockLocatorReport(request.SiteCode).ConvertToCsvList();
            return new SuccessResult<IEnumerable<IEnumerable<string>>>(result);
        }
    }
}
