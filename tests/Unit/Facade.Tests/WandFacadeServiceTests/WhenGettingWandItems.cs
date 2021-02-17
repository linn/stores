namespace Linn.Stores.Facade.Tests.WandFacadeServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Wand.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingWandItems : ContextBase
    {
        private List<WandItem> items;

        private IResult<IEnumerable<WandItem>> results;

        private int consignmentId = 567;

        [SetUp]
        public void SetUp()
        {
            this.items = new List<WandItem>
                                    {
                                        new WandItem { PartNumber = "p1" },
                                        new WandItem { PartNumber = "p2" }
                                    };
            this.WandItemsRepository.FilterBy(Arg.Any<Expression<Func<WandItem, bool>>>())
                .Returns(this.items.AsQueryable());

            this.results = this.Sut.GetWandItems(this.consignmentId);
        }

        [Test]
        public void ShouldCallRepository()
        {
            this.WandItemsRepository.Received().FilterBy(Arg.Any<Expression<Func<WandItem, bool>>>());
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.results.Should().BeOfType<SuccessResult<IEnumerable<WandItem>>>();
            var dataResult = ((SuccessResult<IEnumerable<WandItem>>)this.results).Data;
            dataResult.Should().HaveCount(2);
        }
    }
}
