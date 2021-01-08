namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Stores.Domain.LinnApps.Reports;

    public class  StoragePlaceAuditReportFacadeService : IStoragePlaceAuditReportFacadeService
    {
        private readonly IStoragePlaceAuditReportService storagePlaceAuditReportService;

        public StoragePlaceAuditReportFacadeService(IStoragePlaceAuditReportService storagePlaceAuditReportService)
        {
            this.storagePlaceAuditReportService = storagePlaceAuditReportService;
        }

        public IResult<ResultsModel> GetStoragePlaceAuditReport(IEnumerable<string> locationList, string locationRange)
        {
            return new SuccessResult<ResultsModel>(
                this.storagePlaceAuditReportService.StoragePlaceAuditReport(locationList, locationRange));
        }
    }
}