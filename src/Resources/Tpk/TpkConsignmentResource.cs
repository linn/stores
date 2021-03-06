﻿namespace Linn.Stores.Resources.Tpk
{
    public class TpkConsignmentResource
    {
        public int ConsignmentId { get; set; }

        public int? SalesAccountId { get; set; }

        public int? AddressId { get; set; }

        public string Country { get; set; }

        public string CountryCode { get; set; }

        public decimal TotalNettValue { get; set; }

        public string CurrencyCode { get; set; }
    }
}
