namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps.Requisitions;
    using Linn.Stores.Resources.Requisitions;

    public class RequisitionMovesResourceBuilder : IRequisitionMovesResourceBuilder
    {
        public RequisitionResource Build(RequisitionHeader requisition)
        {
            return new RequisitionResource
            {
                ReqNumber = requisition.ReqNumber,
                Document1 = requisition.Document1,
                Links = this.BuildLinks(requisition).ToArray()
            };
        }

        public string GetLocation(RequisitionHeader requisitionHeader)
        {
            return $"/logistics/requisitions/{requisitionHeader.ReqNumber}";
        }

        object IResourceBuilder<RequisitionHeader>.Build(RequisitionHeader requisition) => this.Build(requisition);

        private IEnumerable<LinkResource> BuildLinks(RequisitionHeader requisition)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(requisition) };
        }
    }
}
