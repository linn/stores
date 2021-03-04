namespace Linn.Stores.Domain.LinnApps
{
    using System;

    public class SalesAccount
    {
        public int AccountId { get; set; }

        public string AccountName { get; set; }

        public string AccountType { get; set; }

        public DateTime? DateClosed { get; set; }
    }
}