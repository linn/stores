namespace Linn.Stores.Service.Tests.StockLocatorsModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Resources.StockLocators;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;
    using NSubstitute.ReturnsExtensions;

    using NUnit.Framework;

    public class WhenGettingLocatorLocationsAndNoProduct : ContextBase
    {
        private StockLocatorWithStoragePlaceInfo stockLocator1;

        [SetUp]
        public void SetUp()
        {
            this.stockLocator1 = new StockLocatorWithStoragePlaceInfo
            {
                LocationId = 1,
                PartNumber = "A",
                Part = new Part { PartNumber = "A" }
            };

            this.ProductService.GetLinkToProduct("A").ReturnsNull();

            this.StockLocatorFacadeService.GetStockLocations(Arg.Any<StockLocatorQueryResource>())
                .Returns(new
                    SuccessResult<IEnumerable<StockLocator>>(new List<StockLocator>
                                                                          {
                                                                              this.stockLocator1
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
        public void ShouldNotHaveProductLink()
        {
            var resultResource = this.Response.Body.DeserializeJson<IEnumerable<StockLocatorResource>>().ToList();
            resultResource.Should().HaveCount(1);
            resultResource.First().Links.Any(l => l.Rel == "product").Should().BeFalse();
        }
    }
}
