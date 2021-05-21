namespace Linn.Stores.Domain.LinnApps
{
    using System;
    using System.Collections.Generic;

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
    }
}
