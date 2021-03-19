namespace Linn.Stores.Facade.Tests.AvailableStockFacadeServiceTests
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

    public class WhenGettingAvailableStock : ContextBase
    {
        private readonly string partNumber = "pn";

        private List<AvailableStock> items;

        private IResult<IEnumerable<AvailableStock>> results;

        [SetUp]
        public void SetUp()
        {
            this.items = new List<AvailableStock>
                                    {
                                        new AvailableStock { LocationCode = "1" },
                                        new AvailableStock { LocationCode = "2" }
                                    };
            this.AvailableStockRepository.FilterBy(Arg.Any<Expression<Func<AvailableStock, bool>>>())
                .Returns(this.items.AsQueryable());

            this.results = this.Sut.GetAvailableStock(this.partNumber);
        }

        [Test]
        public void ShouldCallRepository()
        {
            this.AvailableStockRepository.Received().FilterBy(Arg.Any<Expression<Func<AvailableStock, bool>>>());
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.results.Should().BeOfType<SuccessResult<IEnumerable<AvailableStock>>>();
            var dataResult = ((SuccessResult<IEnumerable<AvailableStock>>)this.results).Data;
            dataResult.Should().HaveCount(2);
        }
    }
}
