namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps.Workstation.Models;
    using Linn.Stores.Resources.Workstation;

    public class WorkstationTopUpStatusResourceBuilder : IResourceBuilder<ResponseModel<WorkstationTopUpStatus>>
    {
        public WorkstationTopUpStatusResource Build(WorkstationTopUpStatus workstationTopUpStatus)
        {
            return new WorkstationTopUpStatusResource
            {
                ProductionTriggerRunJobRef = workstationTopUpStatus.ProductionTriggerRunJobRef,
                ProductionTriggerRunMessage = workstationTopUpStatus.ProductionTriggerRunMessage,
                WorkstationTopUpJobRef = workstationTopUpStatus.WorkstationTopUpJobRef,
                WorkstationTopUpMessage = workstationTopUpStatus.WorkstationTopUpMessage
            };
        }

        public string GetLocation(ResponseModel<WorkstationTopUpStatus> workstationTopUpStatus)
        {
            return $"/logistics/workstations/top-up/{workstationTopUpStatus.ResponseData.WorkstationTopUpJobRef}";
        }

        object IResourceBuilder<ResponseModel<WorkstationTopUpStatus>>.Build(
            ResponseModel<WorkstationTopUpStatus> workstationTopUpStatus) =>
            this.Build(workstationTopUpStatus.ResponseData);

        private IEnumerable<LinkResource> BuildLinks(ResponseModel<WorkstationTopUpStatus> workstationTopUpStatus)
        {
            yield return new LinkResource { Rel = "start-top-up", Href = this.GetLocation(workstationTopUpStatus) };
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(workstationTopUpStatus) };
            yield return new LinkResource { Rel = "status", Href = "/logistics/workstations/top-up" };
        }
    }
}
