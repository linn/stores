namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Allocation;

    public interface ISosAllocHeadFacadeService
    {
        SuccessResult<IEnumerable<SosAllocHead>> GetAllocHeads(int jobId);

        SuccessResult<IEnumerable<SosAllocHead>> GetAllAllocHeads();
    }
}
