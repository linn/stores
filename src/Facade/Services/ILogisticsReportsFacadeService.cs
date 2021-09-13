namespace Linn.Stores.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Consignments.Models;

    public interface ILogisticsReportsFacadeService
    {
        IResult<PackingList> GetPackingList(int consignmentId);
    }
}
