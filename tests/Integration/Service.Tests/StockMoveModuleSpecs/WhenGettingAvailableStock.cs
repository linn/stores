namespace Linn.Stores.Service.Tests.StockMoveModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockMove.Models;
    using Linn.Stores.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingAvailableStock : ContextBase
    {
        private string partNumber;

        private List<AvailableStock> stock;
        [SetUp]
        public void SetUp()
        {
            this.partNumber = "pn";
            this.stock = new List<AvailableStock>
                             {
                                 new AvailableStock { LocationId = 1, QuantityAvailable = 2 },
                                 new AvailableStock { LocationId = 3, QuantityAvailable = 5 }
                             };
            this.AvailableStockFacadeService.GetAvailableStock(this.partNumber)
                .Returns(new SuccessResult<IEnumerable<AvailableStock>>(this.stock.AsQueryable()));

            this.Response = this.Browser.Get(
                "/inventory/available-stock",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.Query("searchTerm", this.partNumber);
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
            this.AvailableStockFacadeService.Received().GetAvailableStock(this.partNumber);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resultResource = this.Response.Body.DeserializeJson<IEnumerable<AvailableStockResource>>().ToList();
            resultResource.Should().HaveCount(2);
            resultResource.First(a => a.LocationId == 1).QuantityAvailable.Should().Be(2);
            resultResource.First(a => a.LocationId == 3).QuantityAvailable.Should().Be(5);
        }
    }
}
