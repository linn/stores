﻿namespace Linn.Stores.Domain.LinnApps.Tests.StockLocatorServiceTests
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.StockLocators;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenSearchingStockLocatorsAndPartNumberInQuery : ContextBase
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
                            PartNumber = "PART A"
                        },
                    new StockLocatorBatch
                        {
                            StockPoolCode = "LINN",
                            LocationId = 1,
                            State = "FREE",
                            BatchRef = "BATCH",
                            PartNumber = "PART B"
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
                partNumber: "PART A",
                locationId: 1,
                palletNumber: null,
                stockPool: "LINN",
                stockState: "FREE",
                batchRef: "BATCH",
                queryBatchView: true);
        }

        [Test]
        public void ShouldReturnOnlyResultForSpecifiedPart()
        {
            this.testResult.Count().Should().Be(1);
            this.testResult.All(x => x.PartNumber.Equals("PART A")).Should().Be(true);
        }
    }
}