namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps.StockMove.Models;
    using Linn.Stores.Resources;

    public class RequisitionProcessResultResourceBuilder : IResourceBuilder<RequisitionProcessResult>
    {
        public ProcessResultResource Build(RequisitionProcessResult process)
        {
            return new ProcessResultResource
            {
               Message = process.Message,
               Success = process.Success,
               Links = this.BuildLinks(process).ToArray()
            };
        }

        public string GetLocation(RequisitionProcessResult process)
        {
            return $"/logistics/requisitions/{process.ReqNumber}";
        }

        object IResourceBuilder<RequisitionProcessResult>.Build(RequisitionProcessResult process) => this.Build(process);

        private IEnumerable<LinkResource> BuildLinks(RequisitionProcessResult process)
        {
            yield return new LinkResource { Rel = "requisition", Href = this.GetLocation(process) };
            yield return new LinkResource { Rel = "req-moves", Href = $"{this.GetLocation(process)}/moves" };
        }
    }
}
