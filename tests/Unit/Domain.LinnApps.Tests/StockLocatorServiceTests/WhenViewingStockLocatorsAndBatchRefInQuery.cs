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
        private readonly IQueryable<StockLocatorBatchesViewModel> repositoryResult =
            new List<StockLocatorBatchesViewModel>().AsQueryable();

        [SetUp]
        public void SetUp()
        {
            this.StockLocatorBatchesView
                .FilterBy(Arg.Any<Expression<Func<StockLocatorBatchesViewModel, bool>>>())
                .Returns(this.repositoryResult);
            this.Sut.GetStockLocatorLocationsView("PART", null, null, null, "ref");
        }

        [Test]
        public void ShouldQueryBatchesView()
        {
            this.StockLocatorBatchesView.Received()
                .FilterBy(Arg.Any<Expression<Func<StockLocatorBatchesViewModel, bool>>>());
        }
    }
}
