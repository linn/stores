namespace Linn.Stores.Domain.LinnApps.ExternalServices
{
    using Linn.Stores.Domain.LinnApps.Models;

    public interface IConsignmentProxyService
    {
        ProcessResult CanCloseAllocation(int consignmentId);
    }
}
