namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.InterCompanyInvoices;
    using Linn.Stores.Resources;

    public class InterCompanyInvoicesResourceBuilder : IResourceBuilder<IEnumerable<InterCompanyInvoice>>
    {
        private readonly InterCompanyInvoiceResourceBuilder interCompanyInvoiceResourceBuilder = new InterCompanyInvoiceResourceBuilder();

        public IEnumerable<IntercompanyInvoiceResource> Build(IEnumerable<InterCompanyInvoice> invoices)
        {
            return invoices.Select(i => this.interCompanyInvoiceResourceBuilder.Build(i));
        }

        public string GetLocation(IEnumerable<InterCompanyInvoice> model)
        {
            throw new System.NotImplementedException();
        }

        object IResourceBuilder<IEnumerable<InterCompanyInvoice>>.Build(IEnumerable<InterCompanyInvoice> invoices) =>
            this.Build(invoices);
    }
}