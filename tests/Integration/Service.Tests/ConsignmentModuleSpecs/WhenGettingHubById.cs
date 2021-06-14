namespace Linn.Stores.Service.Tests.ConsignmentModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Resources.Consignments;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingHubById : ContextBase
    {
        private Hub hub;

        private int hubId;

        [SetUp]
        public void SetUp()
        {
            this.hubId = 145;
            this.hub = new Hub { HubId = this.hubId };

            this.HubFacadeService.GetById(this.hubId)
                .Returns(new SuccessResult<Hub>(this.hub));

            this.Response = this.Browser.Get(
                $"/logistics/hubs/{this.hubId}",
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
            this.HubFacadeService.Received().GetById(this.hubId);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resultResource = this.Response.Body.DeserializeJson<HubResource>();
            resultResource.HubId.Should().Be(this.hubId);
        }
    }
}
