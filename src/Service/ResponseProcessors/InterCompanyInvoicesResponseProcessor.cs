namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps;

    public class InterCompanyInvoicesResponseProcessor : JsonResponseProcessor<IEnumerable<InterCompanyInvoice>>
    {
        public InterCompanyInvoicesResponseProcessor(IResourceBuilder<IEnumerable<InterCompanyInvoice>> resourceBuilder)
            : base(resourceBuilder, "inter-company-invoices", 1)
        {
        }
    }
}