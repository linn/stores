namespace Linn.Stores.Domain.LinnApps
{
    using System.Collections.Generic;

    public class Currency
    {
        public string CurrencyCode { get; set; }

        public string CurrencyName { get; set; }

        public List<InterCompanyInvoice> InterCompanies { get; set; }
    }
}
