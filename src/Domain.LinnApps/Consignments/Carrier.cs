namespace Linn.Stores.Domain.LinnApps.Consignments
{
    using System;

    public class Carrier
    {
        public string CarrierCode { get; set; }

        public string Name { get; set; }

        public DateTime? DateInvalid { get; set; }
    }
}
