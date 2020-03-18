namespace Linn.Stores.Service.Tests.PartsModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdating : ContextBase
    {
        private PartResource requestResource;

        [SetUp]
        public void SetUp()
        {
            this.requestResource = new PartResource { Id = 1, Description = "Desc" };
            var part = new Part { Id = 1, Description = "Desc" };
            this.PartsFacadeService.Update(1, Arg.Any<PartResource>())
                .Returns(new SuccessResult<Part>(part));

            this.Response = this.Browser.Put(
                "/parts/1",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Header("Content-Type", "application/json");
                        with.JsonBody(this.requestResource);
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
            this.PartsFacadeService
                .Received()
                .Update(1, Arg.Is<PartResource>(r => r.Id == this.requestResource.Id));
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<PartResource>();
            resource.Id.Should().Be(1);
            resource.Description.Should().Be("Desc");
        }
    }
}