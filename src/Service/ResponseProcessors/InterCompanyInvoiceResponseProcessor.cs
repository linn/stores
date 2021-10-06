namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.InterCompanyInvoices;

    public class InterCompanyInvoiceResponseProcessor : JsonResponseProcessor<InterCompanyInvoice>
    {
        public InterCompanyInvoiceResponseProcessor(IResourceBuilder<InterCompanyInvoice> resourceBuilder)
        : base(resourceBuilder, "inter-company-invoice", 1)
        {
        }
    }
}