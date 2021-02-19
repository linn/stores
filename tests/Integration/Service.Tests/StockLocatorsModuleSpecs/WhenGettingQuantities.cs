namespace Linn.Stores.Service.Tests.StockLocatorsModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Resources.RequestResources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingQuantities : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.QuantitiesService
                .GetStockQuantities(Arg.Any<string>()).Returns(new SuccessResult<StockQuantities>(
                    new StockQuantities
                    {
                        GoodStock = 1000
                    }));

            this.Response = this.Browser.Get(
                "/inventory/stock-quantities/",
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
            this.QuantitiesService.Received().GetStockQuantities(Arg.Any<string>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resultResource = this.Response.Body.DeserializeJson<StockQuantities>();
            resultResource.GoodStock.Should().Be(1000);
        }
    }
}
