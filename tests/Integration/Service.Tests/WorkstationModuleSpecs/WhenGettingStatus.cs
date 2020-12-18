namespace Linn.Stores.Service.Tests.WorkstationModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Workstation.Models;
    using Linn.Stores.Resources.Workstation;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingStatus : ContextBase
    {
        private ResponseModel<WorkstationTopUpStatus> workstationStatus;

        [SetUp]
        public void SetUp()
        {
            this.workstationStatus = new ResponseModel<WorkstationTopUpStatus>(
                new WorkstationTopUpStatus { ProductionTriggerRunJobRef = "a", WorkstationTopUpJobRef = "b" },
                new List<string>());
            this.WorkstationFacadeService.GetStatus(Arg.Any<IEnumerable<string>>())
                .Returns(new SuccessResult<ResponseModel<WorkstationTopUpStatus>>(this.workstationStatus));

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
        public void ShouldCallService()
        {
            this.WorkstationFacadeService.Received()
                .GetStatus(Arg.Is<IEnumerable<string>>(s => s.Contains("p1")));
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resultResource = this.Response.Body.DeserializeJson<WorkstationTopUpStatusResource>();
            resultResource.ProductionTriggerRunJobRef.Should().Be("a");
            resultResource.WorkstationTopUpJobRef.Should().Be("b");
        }
    }
}
