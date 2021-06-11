namespace Linn.Stores.Domain.LinnApps.Consignments
{
    public class Hub
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
