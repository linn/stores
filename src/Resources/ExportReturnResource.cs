namespace Linn.Stores.Resources
{
    using System.Collections.Generic;

    using Linn.Common.Resources;

    public class ExportReturnResource : HypermediaResource
    {
        public string CarrierCode { get; set; }

        public int ReturnId { get; set; }

        public string DateCreated { get; set; }

        public string Currency { get; set; }

        public int AccountId { get; set; }

        public int? HubId { get; set; }

        public int? OutletNumber { get; set; }

        public string DateDispatched { get; set; }

        public string DateCancelled { get; set; }

        public string CarrierRef { get; set; }

        public string Terms { get; set; }

        public int? NumPallets { get; set; }

        public int? NumCartons { get; set; }

        public double? GrossWeightKg { get; set; }

        public double? GrossDimsM3 { get; set; }

        public string MadeIntercompanyInvoices { get; set; }

        public string DateProcessed { get; set; }

        public string ReturnForCredit { get; set; }

        public string ExportCustomsEntryCode { get; set; }

        public string ExportCustomsEntryDate { get; set; }

        public IEnumerable<ExportReturnDetailResource> ExportReturnDetails { get; set; }

        public EmployeeResource RaisedBy { get; set; }

        public SalesOutletResource SalesOutlet { get; set; }
    }
}