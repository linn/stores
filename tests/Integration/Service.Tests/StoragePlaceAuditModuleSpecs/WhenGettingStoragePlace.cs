namespace Linn.Stores.Service.Tests.StoragePlaceAuditModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.StockLocators;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingStoragePlace : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var sp1 = new StoragePlace { Name = "sp1", PalletNumber = 298};

            this.StoragePlaceService
                .GetStoragePlace(Arg.Any<StoragePlaceRequestResource>()).Returns(
                new SuccessResult<StoragePlace>(sp1));

            this.Response = this.Browser.Get(
                "/inventory/storage-place",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("palletNumber", "298");
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
            this.StoragePlaceService
                .Received().GetStoragePlace(Arg.Any<StoragePlaceRequestResource>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<StoragePlaceResource>();
            resource.PalletNumber.Should().Be(298);
        }
    }
}
