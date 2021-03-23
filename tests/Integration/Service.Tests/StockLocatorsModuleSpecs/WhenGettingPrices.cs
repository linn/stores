namespace Linn.Stores.Service.Tests.StockLocatorsModuleSpecs
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

    public class WhenGettingPrices : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var result = new List<StockLocatorPrices> { new StockLocatorPrices { PartPrice = 0.5m } };
            this.PricesService.GetPrices(Arg.Any<StockLocatorResource>())
                .Returns(new SuccessResult<IEnumerable<StockLocatorPrices>>(result));

            this.Response = this.Browser.Get(
                "/inventory/stock-locators/prices",
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
            this.PricesService.Received().GetPrices(Arg.Any<StockLocatorResource>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resultResource = this.Response.Body.DeserializeJson<IEnumerable<StockLocatorPrices>>();
            resultResource.First().PartPrice.Should().Be(0.5m);
        }
    }
}
