namespace Linn.Stores.Service.Tests.PartsModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenCheckingIfPartCanBeMadeLive : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.PartLiveService.CheckIfPartCanBeMadeLive(Arg.Any<int>())
                .Returns(new SuccessResult<PartLiveTest>(new PartLiveTest
                                                             {
                                                                 Result = true,
                                                                 Message = "Do it"
                                                             }));
            this.Response = this.Browser.Get(
                "/parts/can-be-made-live/1",
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
            this.PartLiveService
                .Received()
                .CheckIfPartCanBeMadeLive(Arg.Any<int>());
        }

        [Test]
        public void ShouldReturnResource()
        {
           var resource = this.Response.Body.DeserializeJson<PartLiveTestResource>();
           resource.CanMakeLive.Should().Be(true);
        }
    }
}