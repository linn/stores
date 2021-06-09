namespace Linn.Stores.Domain.LinnApps.Consignments
{
    using System;
    using System.Collections.Generic;

    using Linn.Stores.Domain.LinnApps.ConsignmentShipfiles;

    public class Consignment
    {
        public int ConsignmentId { get; set; }

        public int? SalesAccountId { get; set; }

        public SalesAccount SalesAccount { get; set; }

        public int? AddressId { get; set; }

        public ConsignmentShipfile Shipfile { get; set; }

        public DateTime? DateClosed { get; set; }

        public string CustomerName { get; set; }

        public IEnumerable<Invoice> Invoices { get; set; }

        public IEnumerable<ConsignmentItem> Items { get; set; }
        
        public Address Address { get; set; }

        public string Carrier { get; set; }

        public string ShippingMethod { get; set; }

        public string Terms { get; set; }

        public string Status { get; set; }

        public DateTime DateOpened { get; set; }

        public Employee ClosedBy { get; set; }

        public int? ClosedById { get; set; }

        public string DespatchLocationCode { get; set; }

        public string Warehouse { get; set; }

        public int? HubId { get; set; }
    }
}
