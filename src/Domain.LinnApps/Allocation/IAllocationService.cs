namespace Linn.Stores.Domain.LinnApps.Allocation
{
    using System;

    using Linn.Stores.Domain.LinnApps.Allocation.Models;

    public interface IAllocationService
    {
        AllocationResult StartAllocation(
            string stockPoolCode,
            string despatchLocationCode,
            int? accountId,
            string articleNumber,
            string accountingCompany,
            string countryCode,
            DateTime? cutOffDate,
            bool excludeUnsuppliableLines,
            bool excludeOnHold,
            bool excludeOverCreditLimit,
            bool excludeNorthAmerica);

        AllocationResult FinishAllocation(int jobId);
    }
}
