namespace Linn.Stores.Domain.LinnApps.Allocation
{
    using Linn.Stores.Domain.LinnApps.Allocation.Models;

    public interface IAllocationService
    {
        AllocationStart StartAllocation(
            string stockPoolCode,
            string despatchLocationCode,
            int? accountId,
            string articleNumber);
    }
}
