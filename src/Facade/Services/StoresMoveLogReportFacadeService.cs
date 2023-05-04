namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Common.Reporting.Resources.Extensions;
    using Linn.Stores.Domain.LinnApps.Reports;
    using Linn.Stores.Resources.RequestResources;
    using Org.BouncyCastle.Asn1.Ocsp;

    public class StoresMoveLogReportFacadeService : IStoresMoveLogReportFacadeService
    {
        private readonly IStoresMoveLogReportService domainService;

        public StoresMoveLogReportFacadeService(IStoresMoveLogReportService domainService)
        {
            this.domainService = domainService;
        }

        public IResult<ResultsModel> GetReport(StoresMoveLogReportRequestResource request)
        {
            return new SuccessResult<ResultsModel>(this.domainService.GetReport(Convert.ToDateTime(request.From), Convert.ToDateTime(request.To), request.PartNumber, request.TransType, request.Location, request.StockPool));
        }

        public IResult<IEnumerable<IEnumerable<string>>> GetExport(StoresMoveLogReportRequestResource request)
        {
            return new SuccessResult<IEnumerable<IEnumerable<string>>>(
                this.domainService.GetReport(Convert.ToDateTime(request.From), Convert.ToDateTime(request.To), request.PartNumber, request.TransType, request.Location, request.StockPool).ConvertToCsvList());
        }
    }
}
