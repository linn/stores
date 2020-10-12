namespace Linn.Stores.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;

    public interface IWhatWillDecrementReportFacadeService
    {
        IResult<ResultsModel> GetWhatWillDecrementReport(
            string partNumber,
            int quantity,
            string typeOfRun,
            string workstationCode);
    }
}
