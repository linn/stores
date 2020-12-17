namespace Linn.Stores.Domain.LinnApps.Tests.AllocationServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Allocation;
    using Linn.Stores.Domain.LinnApps.Exceptions;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUnpickingItemsFails : ContextBase
    {
        private IEnumerable<SosAllocDetail> details;

        private SosAllocDetail detail1;

        private Action action;

        [SetUp]
        public void SetUp()
        {
            this.detail1 = new SosAllocDetail
                               {
                                   Id = 1,
                                   QuantityToAllocate = 10,
                                   MaximumQuantityToAllocate = 12,
                                   AllocationSuccessful = "Y"
                               };
            this.details = new List<SosAllocDetail> { this.detail1 };
            this.SosAllocDetailRepository.FilterBy(Arg.Any<Expression<Func<SosAllocDetail, bool>>>())
                .Returns(this.details.AsQueryable());

            this.action = () => this.Sut.UnpickItems(1, 2, 3);
        }

        [Test]
        public void ShouldThrowException()
        {
            this.action.Should().Throw<UnpickItemsException>("Allocation has been completed");
        }
    }
}
