namespace Linn.Stores.Domain.LinnApps
{
    using System;
    using System.Collections.Generic;

    using Linn.Stores.Domain.LinnApps.Consignments;

    public class Invoice
    {
        public int DocumentNumber { get; set; }

        public string DocumentType { get; set; }

        public int? ConsignmentId { get; set; }

        public SalesAccount Account { get; set; }

        public int AccountId { get; set; }

        public Consignment Consignment { get; set; }

        public IEnumerable<InvoiceDetail> Details { get; set; }

        public DateTime DocumentDate { get; set; }

        public Address DeliveryAddress { get; set; }

        public int DeliveryAddressId { get; set; }
    }
}
