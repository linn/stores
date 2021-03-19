namespace Linn.Stores.Domain.LinnApps.Tests.StockLocatorServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.StockLocators;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenSearchingBatches : ContextBase
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
                            PartNumber = "PART"
                        },
                }.AsQueryable();

        private IEnumerable<StockLocator> testResult;

        [SetUp]
        public void SetUp()
        {
            this.StockLocatorBatchesView
                .FilterBy(Arg.Any<Expression<Func<StockLocatorBatch, bool>>>())
                .Returns(this.repositoryResult);
            
            this.testResult = this.Sut.SearchStockLocatorBatchView(
                partNumber: "PART", 
                locationId: 1, 
                palletNumber: null, 
                stockPool: "LINN", 
                stockState: "FREE",
                category: null);
        }

        [Test]
        public void ShouldQueryBatchesView()
        {
            this.StockLocatorBatchesView.Received()
                .FilterBy(Arg.Any<Expression<Func<StockLocatorBatch, bool>>>());
        }

        [Test]
        public void ShouldReturnCorrectResult()
        {
            this.testResult.Count().Should().Be(1);
        }
    }
}
