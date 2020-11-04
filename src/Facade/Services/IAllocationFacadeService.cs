namespace Linn.Stores.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Allocation.Models;
    using Linn.Stores.Resources.Allocation;

    public interface IAllocationFacadeService
    {
        SuccessResult<AllocationStart> StartAllocation(AllocationOptionsResource allocationOptionsResource);
    }
}
