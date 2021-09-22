namespace Linn.Stores.Domain.LinnApps
{
    using System;
    using System.Collections.Generic;

    using Linn.Stores.Domain.LinnApps.Consignments;

    public class Address
    {
        public int Id { get; set; }

        public string Addressee { get; set; }

        public string PostCode { get; set; }

        public string Addressee2 { get; set; }

        public string Line1 { get; set; }

        public string Line2 { get; set; }

        public string Line3 { get; set; }

        public string Line4 { get; set; }

        public string CountryCode { get; set; }

        public Country Country { get; set; }

        public DateTime? DateInvalid { get; set; }

        public List<Consignment> Consignments { get; set; }

        public List<InterCompanyInvoice> InvoiceInterCompanies { get; set; }

        public List<InterCompanyInvoice> DeliveryInterCompanies { get; set; }
    }
}
