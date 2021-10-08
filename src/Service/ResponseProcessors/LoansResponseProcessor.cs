namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps;

    public class LoansResponseProcessor : JsonResponseProcessor<IEnumerable<Loan>>
    {
        public LoansResponseProcessor(IResourceBuilder<IEnumerable<Loan>> resourceBuilder)
            : base(resourceBuilder, "linnapps-loans", 1)
        {
        }
    }
}
