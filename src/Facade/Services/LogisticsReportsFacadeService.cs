namespace Linn.Stores.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Domain.LinnApps.Consignments.Models;

    public class LogisticsReportsFacadeService : ILogisticsReportsFacadeService
    {
        private readonly IConsignmentService consignmentService;

        public LogisticsReportsFacadeService(IConsignmentService consignmentService)
        {
            this.consignmentService = consignmentService;
        }

        public IResult<PackingList> GetPackingList(int consignmentId)
        {
            return new SuccessResult<PackingList>(this.consignmentService.GetPackingList(consignmentId));
        }
    }
}
