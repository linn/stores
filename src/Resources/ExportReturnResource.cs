namespace Linn.Stores.Resources
{
    using System.Collections.Generic;

    public class ExportReturnResource
    {
        public string CarrierCode { get; set; }

        public int ReturnId { get; set; }

        public string DateCreated { get; set; }

        public string Currency { get; set; }

        public int AccountId { get; set; }

        public int HubId { get; set; }

        public int? OutletNumber { get; set; }

        public string DateDispatched { get; set; }

        public string DateCancelled { get; set; }

        public string CarrierRef { get; set; }

        public string Terms { get; set; }

        public int? NumPallets { get; set; }

        public int? NumCartons { get; set; }

        public int? GrossWeightKg { get; set; }

        public int? GrossDimsM3 { get; set; }

        public int RaisedBy { get; set; }

        public IEnumerable<ExportReturnDetailResource> ExportReturnDetails { get; set; }
    }
}