namespace Linn.Stores.Service.Tests.AllocationModuleSpecs
{
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Allocation.Models;
    using Linn.Stores.Resources.Allocation;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenFinishingAllocation : ContextBase
    {
        private AllocationResult allocationResult;

        private int jobId;

        [SetUp]
        public void SetUp()
        {
            this.jobId = 283476;
            this.allocationResult = new AllocationResult(this.jobId);

            this.AllocationFacadeService.FinishAllocation(this.jobId)
                .Returns(new SuccessResult<AllocationResult>(this.allocationResult));

            this.Response = this.Browser.Post(
                $"/logistics/allocations/{this.jobId}",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.Header("Content-Type", "application/json");
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
            this.AllocationFacadeService.Received()
                .FinishAllocation(this.jobId);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resultResource = this.Response.Body.DeserializeJson<AllocationResource>();
            resultResource.Id.Should().Be(this.allocationResult.Id);
            resultResource.Links.First(a => a.Rel == "display-results").Href.Should()
                .Be($"/logistics/sos-alloc-heads/{this.allocationResult.Id}");
        }
    }
}
