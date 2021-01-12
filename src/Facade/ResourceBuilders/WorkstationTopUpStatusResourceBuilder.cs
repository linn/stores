namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Authorisation;
    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Workstation.Models;
    using Linn.Stores.Resources.Workstation;

    public class WorkstationTopUpStatusResourceBuilder : IResourceBuilder<ResponseModel<WorkstationTopUpStatus>>
    {
        private readonly IAuthorisationService authorisationService;

        private readonly IWorkstationPack workstationPack;

        public WorkstationTopUpStatusResourceBuilder(
            IAuthorisationService authorisationService,
            IWorkstationPack workstationPack)
        {
            this.authorisationService = authorisationService;
            this.workstationPack = workstationPack;
        }

        public WorkstationTopUpStatusResource Build(ResponseModel<WorkstationTopUpStatus> workstationTopUpStatus)
        {
            return new WorkstationTopUpStatusResource
            {
                ProductionTriggerRunJobRef = workstationTopUpStatus.ResponseData.ProductionTriggerRunJobRef,
                ProductionTriggerRunMessage = workstationTopUpStatus.ResponseData.ProductionTriggerRunMessage,
                WorkstationTopUpJobRef = workstationTopUpStatus.ResponseData.WorkstationTopUpJobRef,
                WorkstationTopUpMessage = workstationTopUpStatus.ResponseData.WorkstationTopUpMessage,
                StatusMessage = workstationTopUpStatus.ResponseData.StatusMessage,
                Links = this.BuildLinks(workstationTopUpStatus).ToArray()
            };
        }

        public string GetLocation(ResponseModel<WorkstationTopUpStatus> workstationTopUpStatus)
        {
            return $"/logistics/workstations/top-up/{workstationTopUpStatus.ResponseData.WorkstationTopUpJobRef}";
        }

        object IResourceBuilder<ResponseModel<WorkstationTopUpStatus>>.Build(
            ResponseModel<WorkstationTopUpStatus> workstationTopUpStatus) =>
            this.Build(workstationTopUpStatus);

        private IEnumerable<LinkResource> BuildLinks(ResponseModel<WorkstationTopUpStatus> workstationTopUpStatus)
        {
            if (string.IsNullOrEmpty(this.workstationPack.TopUpRunProgressStatus())
                && this.authorisationService.HasPermissionFor(
                    AuthorisedAction.WorkstationAdmin,
                    workstationTopUpStatus.Privileges))
            {
                yield return new LinkResource { Rel = "start-top-up", Href = "/logistics/workstations/top-up" };
            }

            yield return new LinkResource { Rel = "self", Href = this.GetLocation(workstationTopUpStatus) };
            yield return new LinkResource { Rel = "status", Href = "/logistics/workstations/top-up" };
        }
    }
}
