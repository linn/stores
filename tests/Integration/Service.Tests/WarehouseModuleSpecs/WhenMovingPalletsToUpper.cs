namespace Linn.Stores.Service.Tests.WarehouseModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenMovingPalletsToUpper : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.WarehouseFacadeService.MoveAllPalletsToUpper()
                .Returns(new SuccessResult<string>("ok"));

            this.Response = this.Browser.Post(
                $"/logistics/wcs/move-all-to-upper",
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
            this.WarehouseFacadeService.Received()
                .MoveAllPalletsToUpper();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resultResource = this.Response.Body.DeserializeJson<SuccessResult<string>>();
            resultResource.Data.Should().Be("ok");
        }
    }
}
