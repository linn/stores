﻿namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;
    using Linn.Stores.Domain.LinnApps.Allocation;
    using Linn.Stores.Domain.LinnApps.Allocation.Models;
    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Resources.Allocation;
    using Linn.Stores.Resources.RequestResources;

    public class AllocationFacadeService : IAllocationFacadeService
    {
        private readonly IAllocationService allocationService;

        private readonly IAllocationReportsService allocationReportsService;

        private readonly IQueryRepository<DespatchPalletQueueScsDetail> dpqRepository;

        public AllocationFacadeService(IAllocationService allocationService, IAllocationReportsService allocationReportsService, IQueryRepository<DespatchPalletQueueScsDetail> dpqRepository)
        {
            this.allocationService = allocationService;
            this.allocationReportsService = allocationReportsService;
            this.dpqRepository = dpqRepository;
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
                allocationOptionsResource.ExcludeNorthAmerica,
                allocationOptionsResource.ExcludeEuropeanUnion));
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

        public IResult<ResultsModel> DespatchPickingSummaryReport()
        {
            return new SuccessResult<ResultsModel>(this.allocationReportsService.DespatchPickingSummary());
        }

        public IResult<DespatchPalletQueueResult> DespatchPalletQueueReport()
        {
            return new SuccessResult<DespatchPalletQueueResult>(this.allocationReportsService.DespatchPalletQueue());
        }

        public IResult<IEnumerable<DespatchPalletQueueScsDetail>> DespatchPalletQueueForScs()
        {
            return new SuccessResult<IEnumerable<DespatchPalletQueueScsDetail>>(this.dpqRepository.FindAll().ToList());
        }
    }
}
