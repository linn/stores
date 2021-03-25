namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Requisitions;
    using Linn.Stores.Facade.ResourceBuilders;

    using Nancy.Responses.Negotiation;

    public class RequisitionMovesResponseProcessor : JsonResponseProcessor<RequisitionHeader>
    {
        public RequisitionMovesResponseProcessor(IRequisitionMovesResourceBuilder resourceBuilder)
            : base(resourceBuilder, new List<MediaRange> { "application/vnd.linn.req-moves-summary+json;version=1" })
        {
        }
    }
}
