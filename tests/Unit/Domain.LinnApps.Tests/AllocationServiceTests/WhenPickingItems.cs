namespace Linn.Stores.Domain.LinnApps.Tests.AllocationServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Allocation;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenPickingItems : ContextBase
    {
        private IEnumerable<SosAllocDetail> details;

        private SosAllocDetail detail1;

        private SosAllocDetail detail2;

        private IEnumerable<SosAllocDetail> results;

        [SetUp]
        public void SetUp()
        {
            this.detail1 = new SosAllocDetail { Id = 1, QuantityToAllocate = 0, MaximumQuantityToAllocate = 2 };
            this.detail2 = new SosAllocDetail { Id = 2, QuantityToAllocate = 1, MaximumQuantityToAllocate = 3 };
            this.details = new List<SosAllocDetail> { this.detail1, this.detail2 };
            this.SosAllocDetailRepository.FilterBy(Arg.Any<Expression<Func<SosAllocDetail, bool>>>())
                .Returns(this.details.AsQueryable());

            this.results = this.Sut.PickItems(1, 2, 3);
        }

        [Test]
        public void ShouldFetchItems()
        {
            this.SosAllocDetailRepository.Received().FilterBy(Arg.Any<Expression<Func<SosAllocDetail, bool>>>());
        }

        [Test]
        public void ShouldUpdateItems()
        {
            this.detail1.QuantityToAllocate.Should().Be(this.detail1.MaximumQuantityToAllocate);
            this.detail2.QuantityToAllocate.Should().Be(this.detail2.MaximumQuantityToAllocate);
        }

        [Test]
        public void ShouldCommitTransaction()
        {
            this.TransactionManager.Received().Commit();
        }

        [Test]
        public void ShouldReturnItems()
        {
            this.results.Should().HaveCount(2);
            this.results.Should().Contain(
                a => a.Id == 1 && a.QuantityToAllocate == this.detail1.MaximumQuantityToAllocate);
            this.results.Should().Contain(
                a => a.Id == 2 && a.QuantityToAllocate == this.detail2.MaximumQuantityToAllocate);
        }
    }
}
