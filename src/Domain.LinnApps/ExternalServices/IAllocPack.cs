namespace Linn.Stores.Domain.LinnApps.ExternalServices
{
    using System;

    public interface IAllocPack
    {
        string GetNotes();

        string GetSosNotes();

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
            out string notes,
            out string sosNotes);
    }
}
