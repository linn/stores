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

    public class WhenGettingBatchesInRotationOrder : ContextBase
    {

        [SetUp]
        public void SetUp()
        {
            this.StockLocatorFacadeService.GetBatchesInRotationOrderByPart(Arg.Any<string>())
                .Returns(new
                    SuccessResult<IEnumerable<StockLocator>>(new List<StockLocator>
                        {
                           new StockLocator { Id = 1 },
                           new StockLocator { Id = 2 }
                        }));

            this.Response = this.Browser.Get(
                "/inventory/stock-locators/rotations",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("partNumber", "RES 213");
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
            this.StockLocatorFacadeService.Received().GetBatchesInRotationOrderByPart(Arg.Any<string>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resultResource = this.Response.Body.DeserializeJson<IEnumerable<StockLocator>>().ToList();
            resultResource.Should().HaveCount(2);
        }
    }
}
