namespace Linn.Stores.Domain.LinnApps
{
    using System;

    public class ExportRsn
    {
        public int? RsnNumber { get; set; }

        public string ReasonCodeAlleged { get; set; }

        public DateTime? DateEntered { get; set; }

        public int? Quantity { get; set; }

        public string ArticleNumber { get; set; }

        public int? AccountId { get; set; }

        public int? OutletNumber { get; set; }

        public string OutletName { get; set; }

        public string Country { get; set; }

        public string CountryName { get; set; }

        public string AccountType { get; set; }
    }
}
