namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;

    using Nancy.Responses.Negotiation;

    public class PartTemplateStateResponseProcessor : JsonResponseProcessor<ResponseModel<PartTemplate>>
    {
        public PartTemplateStateResponseProcessor(IResourceBuilder<ResponseModel<PartTemplate>> resourceBuilder)
            : base(resourceBuilder, new List<MediaRange> { new MediaRange("application/vnd.linn.application-state+json;version=1") })
        {
        }
    }
}
