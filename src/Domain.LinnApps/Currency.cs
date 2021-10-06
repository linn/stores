namespace Linn.Stores.Domain.LinnApps
{
    using Linn.Stores.Domain.LinnApps.InterCompanyInvoices;
    using System.Collections.Generic;

    public class Currency
    {
        public string Code { get; set; }

        public string Name { get; set; }
		
        public List<InterCompanyInvoice> InterCompanies { get; set; }		
    }
}
