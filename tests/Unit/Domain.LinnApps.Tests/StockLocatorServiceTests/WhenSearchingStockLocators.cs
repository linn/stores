namespace Linn.Stores.Domain.LinnApps.Tests.StockLocatorServiceTests
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.StockLocators;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenSearchingStockLocators : ContextBase
    {
        private readonly IQueryable<StockLocatorLocation> repositoryResult =
            new List<StockLocatorLocation>
                {
                    new StockLocatorLocation
                        {
                            StockPoolCode = "LINN",
                            State = "FREE",
                            PartNumber = "PART A"
                        },
                    new StockLocatorLocation
                        {
                            StockPoolCode = "LINN",
                            State = "FREE",
                            PartNumber = "PART B"
                        },
                    new StockLocatorLocation
                        {
                            StockPoolCode = "LINN",
                            State = "FREE",
                            PartNumber = "PART C"
                        },
                }.AsQueryable();

        private IEnumerable<StockLocator> testResult;

        [SetUp]
        public void SetUp()
        {
            this.LocationsViewService.QueryView(
                        Arg.Any<string>(), 
                        null, 
                        null, 
                        Arg.Any<string>(), 
                        Arg.Any<string>(), 
                        Arg.Any<string>())
                .Returns(this.repositoryResult);

            this.testResult = this.Sut.SearchStockLocators(
                partNumber: null,
                locationId: null,
                palletNumber: null,
                stockPool: "LINN",
                stockState: "FREE",
                category: null);
        }

        [Test]
        public void ShouldReturnCorrectResult()
        {
            this.testResult.Count().Should().Be(3);
        }
    }
}
