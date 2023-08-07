namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Common.Reporting.Resources.Extensions;
    using Linn.Stores.Domain.LinnApps.Reports;

    public class StockTriggerLevelsForAStoragePlaceFacadeService 
        : IStockTriggerLevelsForAStoragePlaceFacadeService
    {
        private readonly IStockTriggerLevelsForAStoragePlaceReportService domainService;

        public StockTriggerLevelsForAStoragePlaceFacadeService(
            IStockTriggerLevelsForAStoragePlaceReportService domainService)
        {
            this.domainService = domainService;
        }

        public IResult<ResultsModel> GetReport(string place)
        {
            return new SuccessResult<ResultsModel>(this.domainService.GetReport(place));
        }

        public IResult<IEnumerable<IEnumerable<string>>> GetExport(string place)
        {
            var csv = this.domainService.GetReport(place).ConvertToCsvList();

            return new SuccessResult<IEnumerable<IEnumerable<string>>>(csv);
        }
    }
}
