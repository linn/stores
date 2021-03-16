namespace Linn.Stores.Facade.Tests.StockAvailableFacadeServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockMove.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingStockAvailable : ContextBase
    {
        private readonly string partNumber = "pn";

        private List<StockAvailable> items;

        private IResult<IEnumerable<StockAvailable>> results;

        [SetUp]
        public void SetUp()
        {
            this.items = new List<StockAvailable>
                                    {
                                        new StockAvailable { LocationCode = "1" },
                                        new StockAvailable { LocationCode = "2" }
                                    };
            this.StockAvailableRepository.FilterBy(Arg.Any<Expression<Func<StockAvailable, bool>>>())
                .Returns(this.items.AsQueryable());

            this.results = this.Sut.GetAvailableStock(this.partNumber);
        }

        [Test]
        public void ShouldCallRepository()
        {
            this.StockAvailableRepository.Received().FilterBy(Arg.Any<Expression<Func<StockAvailable, bool>>>());
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.results.Should().BeOfType<SuccessResult<IEnumerable<StockAvailable>>>();
            var dataResult = ((SuccessResult<IEnumerable<StockAvailable>>)this.results).Data;
            dataResult.Should().HaveCount(2);
        }
    }
}
