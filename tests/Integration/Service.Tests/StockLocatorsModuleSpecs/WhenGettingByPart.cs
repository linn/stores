namespace Linn.Stores.Service.Tests.StockLocatorsModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingByPart : ContextBase
    {
        private StockLocator stockLocator1;

        private StockLocator stockLocator2;

        [SetUp]
        public void SetUp()
        {
            this.stockLocator1 = new StockLocator { LocationId = 1, PartNumber = "A" };
            this.stockLocator2 = new StockLocator { LocationId = 2, PartNumber = "A" };

            this.StockLocatorFacadeService.FilterBy(Arg.Any<StockLocatorResource>())
                .Returns(new SuccessResult<IEnumerable<StockLocator>>(new List<StockLocator>
                                                                          {
                                                                              this.stockLocator1, this.stockLocator2
                                                                          }));

            this.Response = this.Browser.Get(
                "/inventory/stock-locators",
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
            this.StockLocatorFacadeService.Received().FilterBy(Arg.Any<StockLocatorResource>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resultResource = this.Response.Body.DeserializeJson<IEnumerable<StockLocator>>().ToList();
            resultResource.Should().HaveCount(2);
        }
    }
}
