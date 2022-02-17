namespace Linn.Stores.Resources.Consignments
{
    using System.Collections.Generic;

    using Linn.Common.Resources;

    public class ConsignmentResource : HypermediaResource
    {
        public int ConsignmentId { get; set; }

        public int? SalesAccountId { get; set; }

        public string DateClosed { get; set; }

        public string CustomerName { get; set; }

        public int? AddressId { get; set; }

        public AddressResource Address { get; set; }

        public string Carrier { get; set; }

        public string ShippingMethod { get; set; }

        public string Terms { get; set; }

        public string Status { get; set; }

        public string DateOpened { get; set; }

        public EmployeeResource ClosedBy { get; set; }

        public string DespatchLocationCode { get; set; }

        public string Warehouse { get; set; }

        public int? HubId { get; set; }

        public string CustomsEntryCodePrefix { get; set; }

        public string CustomsEntryCode { get; set; }

        public string CustomsEntryCodeDate { get; set; }

        public string CarrierRef { get; set; }

        public string MasterCarrierRef { get; set; }

        public IEnumerable<ConsignmentPalletResource> Pallets { get; set; }

        public IEnumerable<ConsignmentItemResource> Items { get; set; }

        public IEnumerable<ExportBookResource> ExportBooks { get; set; }

        public IEnumerable<InvoiceResource> Invoices { get; set; }
    }
}
