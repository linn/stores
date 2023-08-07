namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class PartTemplateResponseProcessor : JsonResponseProcessor<ResponseModel<PartTemplate>>
    {
        public PartTemplateResponseProcessor(IResourceBuilder<ResponseModel<PartTemplate>> resourceBuilder)
            : base(resourceBuilder, "linnapps-part-template", 1)
        {
        }
    }
}
