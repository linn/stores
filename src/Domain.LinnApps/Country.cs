namespace Linn.Stores.Domain.LinnApps
{
    using System;
    using System.Collections.Generic;

    public class Country
    {
        public string CountryCode { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string TradeCurrency { get; set; }

        public string ECMember { get; set; }

        public DateTime? DateInvalid { get; set; }

        public IEnumerable<Consignment> Consignments { get; set; }
    }
}
