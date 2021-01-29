namespace Linn.Stores.Domain.LinnApps.ExternalServices
{
    using System;

    public interface IAllocPack
    {
        int StartAllocation(
            string stockPoolCode,
            string despatchLocation,
            int? accountId,
            int? outletNumber,
            string articleNumber,
            string accountingCompany,
            DateTime? cutOffDate,
            string countryCode,
            bool excludeUnsuppliable,
            bool excludeHold,
            bool excludeOverCredit,
            bool excludeNorthAmerica,
            bool excludeEuropeanUnion,
            out string notes,
            out string sosNotes);

        void FinishAllocation(int jobId, out string notes, out string success);
    }
}
