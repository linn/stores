namespace Linn.Stores.Service.Tests.WorkstationModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Workstation.Models;
    using Linn.Stores.Resources.Workstation;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingStatusWithPrivilegesButRunInProgress : ContextBase
    {
        private ResponseModel<WorkstationTopUpStatus> workstationStatus;

        private WorkstationTopUpStatus status;

        [SetUp]
        public void SetUp()
        {
            this.status = new WorkstationTopUpStatus
                              {
                                  ProductionTriggerRunJobRef = "a",
                                  WorkstationTopUpJobRef = "b",
                                  ProductionTriggerRunMessage = "it was run",
                                  WorkstationTopUpMessage = "so was this",
                                  StatusMessage = "in progress"
                              };
            this.workstationStatus = new ResponseModel<WorkstationTopUpStatus>(
                this.status,
                new List<string>());
            this.WorkstationFacadeService.GetStatus(Arg.Any<IEnumerable<string>>())
                .Returns(new SuccessResult<ResponseModel<WorkstationTopUpStatus>>(this.workstationStatus));
            this.AuthorisationService.HasPermissionFor(AuthorisedAction.WorkstationAdmin, Arg.Any<List<string>>())
                .Returns(true);
            this.WorkstationPack.TopUpRunProgressStatus().Returns("in progress");
            this.Response = this.Browser.Get(
                "/logistics/workstations/top-up",
                with =>
                {
                    with.Header("Accept", "application/json");
                }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resultResource = this.Response.Body.DeserializeJson<WorkstationTopUpStatusResource>();
            resultResource.ProductionTriggerRunJobRef.Should().Be(this.status.ProductionTriggerRunJobRef);
            resultResource.ProductionTriggerRunMessage.Should().Be(this.status.ProductionTriggerRunMessage);
            resultResource.WorkstationTopUpJobRef.Should().Be(this.status.WorkstationTopUpJobRef);
            resultResource.WorkstationTopUpMessage.Should().Be(this.status.WorkstationTopUpMessage);
            resultResource.Links.Length.Should().Be(2);
            resultResource.Links.First(a => a.Rel == "self").Href.Should().Be("/logistics/workstations/top-up/b");
            resultResource.Links.First(a => a.Rel == "status").Href.Should().Be("/logistics/workstations/top-up");
        }
    }
}
