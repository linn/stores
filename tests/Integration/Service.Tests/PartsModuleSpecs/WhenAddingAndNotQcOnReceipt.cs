namespace Linn.Stores.Service.Tests.PartsModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenAddingAndNotQcOnReceipt : ContextBase
    {
        private PartResource requestResource;

        [SetUp]
        public void SetUp()
        {
            this.requestResource = new PartResource { Id = 1, StockControlled = "Y", CreatedBy = 1 };
            var part = new Part
                              {
                                 Id = 1,
                                 StockControlled = "Y",
                                 CreatedBy = new Employee { Id = 1 },
            };
            this.PartsFacadeService.Add(Arg.Any<PartResource>())
                .Returns(new CreatedResult<Part>(part));

            this.Response = this.Browser.Post(
                "/parts",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Header("Content-Type", "application/json");
                        with.JsonBody(this.requestResource);
                    }).Result;
        }

        [Test]
        public void ShouldReturnCreated()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Test]
        public void ShouldCallService()
        {
            this.PartsFacadeService
                .Received()
                .Add(Arg.Is<PartResource>(r => r.Id == this.requestResource.Id));
        }

        [Test]
        public void ShouldNotAddQcInfo()
        {
            this.PartsDomainService.DidNotReceive()
                .AddQcControl(Arg.Any<string>(), Arg.Any<int?>(), Arg.Any<string>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<PartResource>();
            resource.Id.Should().Be(1);
        }
    }
}