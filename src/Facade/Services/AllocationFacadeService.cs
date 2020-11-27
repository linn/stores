namespace Linn.Stores.Facade.Services
{
    using System;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Allocation;
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
            var cutOffDate = !string.IsNullOrWhiteSpace(allocationOptionsResource.CutOffDate)
                                 ? DateTime.Parse(allocationOptionsResource.CutOffDate)
                                 : (DateTime?)null;

            return new SuccessResult<AllocationStart>(this.allocationService.StartAllocation(
                allocationOptionsResource.StockPoolCode,
                allocationOptionsResource.DespatchLocationCode,
                allocationOptionsResource.AccountId,
                allocationOptionsResource.ArticleNumber,
                allocationOptionsResource.AccountingCompany,
                cutOffDate,
                allocationOptionsResource.ExcludeUnsuppliableLines,
                allocationOptionsResource.ExcludeOnHold,
                allocationOptionsResource.ExcludeOverCreditLimit));
        }
    }
}
