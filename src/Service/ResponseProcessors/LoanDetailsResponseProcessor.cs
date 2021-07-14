namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps;

    using Nancy.Responses.Negotiation;

    public class LoanDetailsResponseProcessor : JsonResponseProcessor<IEnumerable<LoanDetail>>
    {
        public LoanDetailsResponseProcessor(IResourceBuilder<IEnumerable<LoanDetail>> resourceBuilder)
            : base(resourceBuilder, "loan-details", 1)
        {
        }
    }
}
