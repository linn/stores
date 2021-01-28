namespace Linn.Stores.Domain.LinnApps.Allocation
{
    using System;
    using System.Collections.Generic;

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
            bool excludeNorthAmerica,
            bool excludeEuropeanUnion);

        AllocationResult FinishAllocation(int jobId);

        IEnumerable<SosAllocDetail> PickItems(int jobId, int accountId, int outletNumber);

        IEnumerable<SosAllocDetail> UnpickItems(int jobId, int accountId, int outletNumber);
    }
}
