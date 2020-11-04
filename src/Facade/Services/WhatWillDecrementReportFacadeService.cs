namespace Linn.Stores.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Stores.Domain.LinnApps.Reports;

    public class WhatWillDecrementReportFacadeService : IWhatWillDecrementReportFacadeService
    {
        private readonly IWhatWillDecrementReportService whatWillDecrementReportService;

        public WhatWillDecrementReportFacadeService(IWhatWillDecrementReportService whatWillDecrementReportService)
        {
            this.whatWillDecrementReportService = whatWillDecrementReportService;
        }

        public IResult<ResultsModel> GetWhatWillDecrementReport(
            string partNumber,
            int quantity,
            string typeOfRun,
            string workstationCode)
        {
            return new SuccessResult<ResultsModel>(
                this.whatWillDecrementReportService.WhatWillDecrementReport(
                    partNumber,
                    quantity,
                    typeOfRun,
                    workstationCode));
        }
    }
}
