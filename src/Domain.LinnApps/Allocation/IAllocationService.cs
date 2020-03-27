namespace Linn.Stores.Domain.LinnApps.Allocation
{
    using System;

    using Linn.Stores.Domain.LinnApps.Allocation.Models;

    public interface IAllocationService
    {
        AllocationStart StartAllocation(
            string stockPoolCode,
            string despatchLocationCode,
            int? accountId,
            string articleNumber,
            string accountingCompany,
            DateTime? cutOffDate);
    }
}
