namespace Linn.Stores.Domain.LinnApps.Consignments
{
    using System;

    public class ShippingTerm
    {
        public int BridgeId { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public DateTime? DateInvalid { get; set; }
    }
}
