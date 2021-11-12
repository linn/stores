namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class PartTemplateResponseProcessor : JsonResponseProcessor<PartTemplate>
    {
        public PartTemplateResponseProcessor(IResourceBuilder<PartTemplate> resourceBuilder)
            : base(resourceBuilder, "linnapps-part-templates", 1)
        {
        }
    }
}
