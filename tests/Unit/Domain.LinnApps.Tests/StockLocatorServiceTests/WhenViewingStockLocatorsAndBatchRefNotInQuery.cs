namespace Linn.Stores.Domain.LinnApps.Tests.StockLocatorServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Stores.Domain.LinnApps.StockLocators;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenViewingStockLocatorsAndBatchRefNotInQuery : ContextBase
    {
        private readonly IQueryable<StockLocatorLocationsViewModel> repositoryResult =
            new List<StockLocatorLocationsViewModel>().AsQueryable();

        [SetUp]
        public void SetUp()
        {
            this.StockLocatorLocationsView
                .FilterBy(Arg.Any<Expression<Func<StockLocatorLocationsViewModel, bool>>>())
                .Returns(this.repositoryResult);
            this.Sut.GetStockLocatorLocationsView("PART", null, null, null, null);
        }

        [Test]
        public void ShouldQueryLocationsView()
        {
            this.StockLocatorLocationsView.Received()
                .FilterBy(Arg.Any<Expression<Func<StockLocatorLocationsViewModel, bool>>>());
        }
    }
}
