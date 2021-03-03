namespace Linn.Stores.Domain.LinnApps.Tests.StockLocatorServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Stores.Domain.LinnApps.StockLocators;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenViewingStockLocatorsAndBatchRefInQuery : ContextBase
    {
        private readonly IQueryable<StockLocatorBatch> repositoryResult =
            new List<StockLocatorBatch>().AsQueryable();

        [SetUp]
        public void SetUp()
        {
            this.StockLocatorBatchesView
                .FilterBy(Arg.Any<Expression<Func<StockLocatorBatch, bool>>>())
                .Returns(this.repositoryResult);
            this.Sut.SearchStockLocators("PART", null, null, null, null, "ref", false);
        }

        [Test]
        public void ShouldQueryBatchesView()
        {
            this.StockLocatorBatchesView.Received()
                .FindAll();
        }
    }
}
