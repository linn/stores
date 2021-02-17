namespace Linn.Stores.Service.Tests.StockLocatorsModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingLocatorLocations : ContextBase
    {
        private StockLocatorWithStoragePlaceInfo stockLocator1;

        private StockLocatorWithStoragePlaceInfo stockLocator2;

        [SetUp]
        public void SetUp()
        {
            this.stockLocator1 = new StockLocatorWithStoragePlaceInfo { LocationId = 1, PartNumber = "A" };
            this.stockLocator2 = new StockLocatorWithStoragePlaceInfo { LocationId = 2, PartNumber = "A" };

            this.StockLocatorFacadeService.GetStockLocations(Arg.Any<StockLocatorResource>())
                .Returns(new
                    SuccessResult<IEnumerable<StockLocator>>(new List<StockLocator>
                                                                          {
                                                                              this.stockLocator1, this.stockLocator2
                                                                          }));

            this.Response = this.Browser.Get(
                "/inventory/stock-locators-by-location/",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.Query("partNumber", "A");
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
            this.StockLocatorFacadeService.Received().GetStockLocations(Arg.Any<StockLocatorResource>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resultResource = this.Response.Body.DeserializeJson<IEnumerable<StockLocator>>().ToList();
            resultResource.Should().HaveCount(2);
        }
    }
}
