namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class PartTqmsOverridesResponseProcessor 
        : JsonResponseProcessor<IEnumerable<PartTqmsOverride>>
    {
        public PartTqmsOverridesResponseProcessor(
            IResourceBuilder<IEnumerable<PartTqmsOverride>> resourceBuilder)
            : base(resourceBuilder, "tqms-categories", 1)
        {
        }
    }
}
