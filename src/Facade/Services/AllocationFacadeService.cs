namespace Linn.Stores.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Allocation.Models;
    using Linn.Stores.Resources.Allocation;

    public class AllocationFacadeService : IAllocationFacadeService
    {
        private readonly IAllocationService allocationService;

        public AllocationFacadeService(IAllocationService allocationService)
        {
            this.allocationService = allocationService;
        }

        public SuccessResult<AllocationStart> StartAllocation(AllocationOptionsResource allocationOptionsResource)
        {
            return new SuccessResult<AllocationStart>(
                this.allocationService.StartAllocation(allocationOptionsResource.StockPool));
        }
    }
}