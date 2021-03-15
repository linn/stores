namespace Linn.Stores.Domain.LinnApps
{
    using System.Collections.Generic;

    using Linn.Stores.Domain.LinnApps.Allocation;

    public class SalesOutlet
    {
        public SalesOutlet()
        {
        }

        public SalesOutlet(int accountId, int outletNumber)
        {
            this.AccountId = accountId;
            this.OutletNumber = outletNumber;
        }

        public int AccountId { get; set; }

        public int OutletNumber { get; set; }

        public string Name { get; set; }

        public int SalesCustomerId { get; set; }

        public string CountryCode { get; set; }

        public string CountryName { get; set; }

        public IEnumerable<SosAllocHead> SosAllocHeads { get; set; }

        public IEnumerable<ExportReturn> ExportReturns { get; set; } 
    }
}
