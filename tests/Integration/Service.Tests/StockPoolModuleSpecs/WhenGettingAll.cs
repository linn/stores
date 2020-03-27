namespace Linn.Stores.Service.Tests.StockPoolModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Allocation;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.Allocation;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingAll : ContextBase
    {
        private StockPool stockPool1;

        private StockPool stockPool2;

        [SetUp]
        public void SetUp()
        {
            this.stockPool1 = new StockPool { Id = 1, StockPoolCode = "one" };
            this.stockPool2 = new StockPool { Id = 2, StockPoolCode = "two" };

            this.StockPoolFacadeService.GetAll()
                .Returns(new SuccessResult<IEnumerable<StockPool>>(new List<StockPool>
                                                                              {
                                                                                  this.stockPool1, this.stockPool2
                                                                              }));

            this.Response = this.Browser.Get(
                "/inventory/stock-pools",
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
            this.StockPoolFacadeService.Received().GetAll();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resultResource = this.Response.Body.DeserializeJson<IEnumerable<StockPoolResource>>().ToList();
            resultResource.Should().HaveCount(2);
            resultResource.First(a => a.Id == 1).StockPoolCode.Should().Be("one");
            resultResource.First(a => a.Id == 2).StockPoolCode.Should().Be("two");
        }
    }
}
