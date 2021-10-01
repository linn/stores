namespace Linn.Stores.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.InterCompanyInvoices;
    using Linn.Stores.Resources;

    public class InterCompanyInvoiceResourceBuilder : IResourceBuilder<InterCompanyInvoice>
    {
        public IntercompanyInvoiceResource Build(InterCompanyInvoice invoice)
        {
            return new IntercompanyInvoiceResource
                       {
                           DocumentNumber = invoice.DocumentNumber
                       };
        }

        public string GetLocation(InterCompanyInvoice model)
        {
            throw new System.NotImplementedException();
        }

        object IResourceBuilder<InterCompanyInvoice>.Build(InterCompanyInvoice invoice) => this.Build(invoice);
    }
}