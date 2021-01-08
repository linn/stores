namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Allocation;
    using Linn.Stores.Domain.LinnApps.Allocation.Models;
    using Linn.Stores.Resources.Allocation;
    using Linn.Stores.Resources.RequestResources;

    public interface IAllocationFacadeService
    {
        IResult<AllocationResult> StartAllocation(AllocationOptionsResource allocationOptionsResource);

        IResult<AllocationResult> FinishAllocation(int jobId);

        IResult<IEnumerable<SosAllocDetail>> PickItems(AccountOutletRequestResource requestResource);

        IResult<IEnumerable<SosAllocDetail>> UnpickItems(AccountOutletRequestResource requestResource);
    }
}
