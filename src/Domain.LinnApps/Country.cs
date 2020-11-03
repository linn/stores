namespace Linn.Stores.Domain.LinnApps
{
    using System;

    public class Country
    {
        public string CountryCode { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string TradeCurrency { get; set; }

        public string ECMember { get; set; }

        public DateTime? DateInvalid { get; set; }
    }
}
