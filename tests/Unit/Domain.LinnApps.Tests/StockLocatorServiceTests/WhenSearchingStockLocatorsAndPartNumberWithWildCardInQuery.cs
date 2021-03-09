namespace Linn.Stores.Domain.LinnApps.Tests.StockLocatorServiceTests
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.StockLocators;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenSearchingStockLocatorsAndPartNumberWithWildcardInQuery : ContextBase
    {
        private readonly IQueryable<StockLocatorBatch> repositoryResult =
            new List<StockLocatorBatch>
                {
                    new StockLocatorBatch
                        {
                            StockPoolCode = "LINN",
                            LocationId = 1,
                            State = "FREE",
                            BatchRef = "BATCH",
                            PartNumber = "RES 211"
                        },
                    new StockLocatorBatch
                        {
                            StockPoolCode = "LINN",
                            LocationId = 1,
                            State = "FREE",
                            BatchRef = "BATCH",
                            PartNumber = "RES 222"
                        },
                    new StockLocatorBatch
                        {
                            StockPoolCode = "LINN",
                            LocationId = 1,
                            State = "FREE",
                            BatchRef = "BATCH",
                            PartNumber = "PART C"
                        },
                }.AsQueryable();

        private IEnumerable<StockLocator> testResult;

        [SetUp]
        public void SetUp()
        {
            this.StockLocatorBatchesView
                .FindAll()
                .Returns(this.repositoryResult);

            this.testResult = this.Sut.SearchStockLocators(
                partNumber: "RES *",
                locationId: 1,
                palletNumber: null,
                stockPool: "LINN",
                stockState: "FREE",
                batchRef: "BATCH",
                queryBatchView: true);
        }

        [Test]
        public void ShouldReturnOnlyMatchingResults()
        {
            this.testResult.Count().Should().Be(2);
            this.testResult.All(x => x.PartNumber.StartsWith("RES")).Should().Be(true);
        }
    }
}
