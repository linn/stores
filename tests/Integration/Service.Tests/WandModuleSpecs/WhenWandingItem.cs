namespace Linn.Stores.Service.Tests.WandModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Wand.Models;
    using Linn.Stores.Resources.Wand;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenWandingItem : ContextBase
    {
        private WandItemRequestResource resource;

        private WandResult result;

        [SetUp]
        public void SetUp()
        {
            this.resource = new WandItemRequestResource { ConsignmentId = 1, WandString = "ws" };
            this.result = new WandResult { Message = "ok", Success = true };
            this.WandFacadeService.WandItem(
                    Arg.Is<WandItemRequestResource>(
                        a => a.ConsignmentId == this.resource.ConsignmentId
                             && a.WandString == this.resource.WandString))
                .Returns(new SuccessResult<WandResult>(this.result));

            this.Response = this.Browser.Post(
                $"/logistics/wand/items",
                with =>
                {
                    with.Header("Accept", "application/json");
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
            this.WandFacadeService.Received().WandItem(Arg.Is<WandItemRequestResource>(
                a => a.ConsignmentId == this.resource.ConsignmentId
                     && a.WandString == this.resource.WandString));
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resultResource = this.Response.Body.DeserializeJson<WandItemResultResource>();
            resultResource.Message.Should().Be(this.result.Message);
            resultResource.Success.Should().Be(this.result.Success);
        }
    }
}
