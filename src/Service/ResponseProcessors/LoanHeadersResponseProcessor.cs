namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps;

    public class LoanHeadersResponseProcessor : JsonResponseProcessor<IEnumerable<LoanHeader>>
    {
        public LoanHeadersResponseProcessor(IResourceBuilder<IEnumerable<LoanHeader>> resourceBuilder)
            : base(resourceBuilder, "linnapps-loan-headers", 1)
        {
        }
    }
}
