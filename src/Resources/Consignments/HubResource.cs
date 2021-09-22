namespace Linn.Stores.Resources.Consignments
{
    using Linn.Common.Resources;

    public class HubResource : HypermediaResource
    {
        public int HubId { get; set; }

        public string Description { get; set; }

        public int OrgId { get; set; }

        public int AddressId { get; set; }

        public string CustomStamp { get; set; }

        public string CarrierCode { get; set; }

        public string EcHub { get; set; }

        public string ReturnAccountingCompany { get; set; }

        public int? ReturnAddressId { get; set; }

        public string ReturnCustomStamp { get; set; }
    }
}
