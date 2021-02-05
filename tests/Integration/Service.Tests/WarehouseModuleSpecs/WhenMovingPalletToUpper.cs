namespace Linn.Stores.Service.Tests.WarehouseModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Resources.RequestResources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenMovingPalletToUpper : ContextBase
    {
        private PalletMoveRequestResource resource;

        [SetUp]
        public void SetUp()
        {
            this.resource = new PalletMoveRequestResource { PalletNumber = 1, Reference = "3" };
            this.WarehouseFacadeService.MovePalletToUpper(this.resource.PalletNumber, this.resource.Reference)
                .Returns(new SuccessResult<string>("ok"));

            this.Response = this.Browser.Post(
                $"/logistics/wcs/move-to-upper",
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
            this.WarehouseFacadeService.Received()
                .MovePalletToUpper(this.resource.PalletNumber, this.resource.Reference);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resultResource = this.Response.Body.DeserializeJson<SuccessResult<string>>();
            resultResource.Data.Should().Be("ok");
        }
    }
}
