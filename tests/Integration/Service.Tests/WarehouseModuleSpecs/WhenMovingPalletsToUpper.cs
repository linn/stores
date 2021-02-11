namespace Linn.Stores.Service.Tests.WarehouseModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Resources;

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
                .Returns(new SuccessResult<MessageResult>(new MessageResult("ok")));

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
            var resultResource = this.Response.Body.DeserializeJson<MessageResource>();
            resultResource.Message.Should().Be("ok");
        }
    }
}
