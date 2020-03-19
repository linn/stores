namespace Linn.Stores.Domain.LinnApps.Allocation.Models
{
    public interface IAllocationService
    {
        AllocationStart StartAllocation(string stockPool);
    }
}
