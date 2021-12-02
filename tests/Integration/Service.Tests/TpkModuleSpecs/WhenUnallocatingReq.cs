namespace Linn.Stores.Service.Tests.TpkModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Resources.RequestResources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUnallocatingReq : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var request = new UnallocateReqRequestResource
            {
               ReqNumber = 1,
               UnallocatedBy = 1
            };

            this.TpkFacadeService.UnallocateReq(Arg.Any<UnallocateReqRequestResource>())
                .Returns(new SuccessResult<ProcessResult>(new ProcessResult(true, string.Empty)));

            this.Response = this.Browser.Post(
                "/logistics/tpk/unallocate-req",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.JsonBody(request);
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
            this.TpkFacadeService.Received().UnallocateReq(Arg.Any<UnallocateReqRequestResource>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ProcessResult>();
            resource.Success.Should().BeTrue();
        }
    }
}
