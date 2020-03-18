namespace Linn.Stores.Service.Tests.AllocationModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Allocation.Models;
    using Linn.Stores.Resources.Allocation;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenStartingAllocation : ContextBase
    {
        private AllocationOptionsResource resource;

        private AllocationStart allocationStartDetails;

        [SetUp]
        public void SetUp()
        {
            this.allocationStartDetails = new AllocationStart { Id = 2934762 };

            this.resource = new AllocationOptionsResource();

            this.AllocationFacadeService.StartAllocation(Arg.Any<AllocationOptionsResource>())
                .Returns(new SuccessResult<AllocationStart>(this.allocationStartDetails));

            this.Response = this.Browser.Post(
                "/logistics/allocations",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.Header("Content-Type", "application/json");
                    with.JsonBody(this.resource);
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
                .StartAllocation(Arg.Is<AllocationOptionsResource>(
                        r => r.StockPool == this.resource.StockPool));
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resultResource = this.Response.Body.DeserializeJson<AllocationStartResource>();
            resultResource.Id.Should().Be(this.allocationStartDetails.Id);
        }
    }
}
