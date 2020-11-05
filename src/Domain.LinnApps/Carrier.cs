namespace Linn.Stores.Domain.LinnApps
{
    using System;

    public class Carrier
    {
        public string CarrierCode { get; set; }

        public string Name { get; set; }

        public int OrganisationId { get; set; }

        public DateTime? DateInvalid { get; set; }
    }
}
