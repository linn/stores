namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Allocation;
    using Linn.Stores.Domain.LinnApps.Allocation.Models;
    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Resources.Allocation;
    using Linn.Stores.Resources.RequestResources;

    public class AllocationFacadeService : IAllocationFacadeService
    {
        private readonly IAllocationService allocationService;

        public AllocationFacadeService(IAllocationService allocationService)
        {
            this.allocationService = allocationService;
        }

        public IResult<AllocationResult> StartAllocation(AllocationOptionsResource allocationOptionsResource)
        {
            var cutOffDate = !string.IsNullOrWhiteSpace(allocationOptionsResource.CutOffDate)
                                 ? DateTime.Parse(allocationOptionsResource.CutOffDate)
                                 : (DateTime?)null;

            return new SuccessResult<AllocationResult>(this.allocationService.StartAllocation(
                allocationOptionsResource.StockPoolCode,
                allocationOptionsResource.DespatchLocationCode,
                allocationOptionsResource.AccountId,
                allocationOptionsResource.ArticleNumber,
                allocationOptionsResource.AccountingCompany,
                allocationOptionsResource.CountryCode,
                cutOffDate,
                allocationOptionsResource.ExcludeUnsuppliableLines,
                allocationOptionsResource.ExcludeOnHold,
                allocationOptionsResource.ExcludeOverCreditLimit,
                allocationOptionsResource.ExcludeNorthAmerica));
        }

        public IResult<AllocationResult> FinishAllocation(int jobId)
        {
            try
            {
                var allocation = this.allocationService.FinishAllocation(jobId);
                return new SuccessResult<AllocationResult>(allocation);
            }
            catch (FinishAllocationException ex)
            {
                return new BadRequestResult<AllocationResult>(ex.Message);
            }
        }

        public IResult<IEnumerable<SosAllocDetail>> PickItems(AccountOutletRequestResource requestResource)
        {
            try
            {
                var items = this.allocationService.PickItems(
                    requestResource.JobId,
                    requestResource.AccountId,
                    requestResource.OutletNumber);
                return new SuccessResult<IEnumerable<SosAllocDetail>>(items);
            }
            catch (PickItemsException ex)
            {
                return new BadRequestResult<IEnumerable<SosAllocDetail>>(ex.Message);
            }
        }

        public IResult<IEnumerable<SosAllocDetail>> UnpickItems(AccountOutletRequestResource requestResource)
        {
            try
            {
                var items = this.allocationService.UnpickItems(
                    requestResource.JobId,
                    requestResource.AccountId,
                    requestResource.OutletNumber);
                return new SuccessResult<IEnumerable<SosAllocDetail>>(items);
            }
            catch (UnpickItemsException ex)
            {
                return new BadRequestResult<IEnumerable<SosAllocDetail>>(ex.Message);
            }
        }
    }
}
