namespace Linn.Stores.Service.Tests.GoodsInModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Resources.StockLocators;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingDemLocations : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var locA = new StorageLocation();
            var locB = new StorageLocation();

            this.StorageLocationService.FilterBy(Arg.Any<StorageLocationResource>())
                .Returns(new SuccessResult<IEnumerable<StorageLocation>>(new List<StorageLocation> { locA, locB }));

            this.Response = this.Browser.Get(
                "/logistics/goods-in/dem-locations",
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
            this.StorageLocationService.Received().FilterBy(Arg.Any<StorageLocationResource>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<StorageLocationResource>>().ToList();
            resource.Should().HaveCount(2);
        }
    }
}
