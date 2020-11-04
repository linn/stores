namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class DecrementRulesResponseProcessor : JsonResponseProcessor<IEnumerable<DecrementRule>>
    {
        public DecrementRulesResponseProcessor(IResourceBuilder<IEnumerable<DecrementRule>> resourceBuilder)
            : base(resourceBuilder, "decrement-rules", 1)
        {
        }
    }
}
