namespace Linn.Stores.Service.Tests.StockLocatorsModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingByPart : ContextBase
    {
        private StockLocatorWithStoragePlaceInfo stockLocator1;

        private StockLocatorWithStoragePlaceInfo stockLocator2;

        [SetUp]
        public void SetUp()
        {
            this.stockLocator1 = new StockLocatorWithStoragePlaceInfo { LocationId = 1, PartNumber = "A" };
            this.stockLocator2 = new StockLocatorWithStoragePlaceInfo { LocationId = 2, PartNumber = "A" };
            this.StockLocatorFacadeService.GetStockLocatorsForPart(Arg.Any<int>())
                .Returns(new 
                    SuccessResult<IEnumerable<StockLocatorWithStoragePlaceInfo>>(new List<StockLocatorWithStoragePlaceInfo>
                                                                          {
                                                                              this.stockLocator1, this.stockLocator2
                                                                          }));

            this.Response = this.Browser.Get(
                "/inventory/stock-locators",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("partId", "1");
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
            this.StockLocatorFacadeService.Received().GetStockLocatorsForPart(Arg.Any<int>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resultResource = this.Response.Body.DeserializeJson<IEnumerable<StockLocatorWithStoragePlaceInfo>>().ToList();
            resultResource.Should().HaveCount(2);
            resultResource.First().PartNumber.Should().Be("A");
        }
    }
}
