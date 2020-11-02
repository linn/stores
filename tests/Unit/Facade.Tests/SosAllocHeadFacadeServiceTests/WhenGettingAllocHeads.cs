namespace Linn.Stores.Facade.Tests.SosAllocHeadFacadeServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Allocation;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingAllocHeads : ContextBase
    {
        private IResult<IEnumerable<SosAllocHead>> results;

        [SetUp]
        public void SetUp()
        {
            this.SosAllocHeadRepository.FilterBy(Arg.Any<Expression<Func<SosAllocHead, bool>>>())
                .Returns(new List<SosAllocHead> { new SosAllocHead(), new SosAllocHead() }.AsQueryable());
            this.results = this.Sut.GetAllocHeads(44);
        }

        [Test]
        public void ShouldCallRepository()
        {
            this.SosAllocHeadRepository.Received().FilterBy(Arg.Any<Expression<Func<SosAllocHead, bool>>>());
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.results.Should().BeOfType<SuccessResult<IEnumerable<SosAllocHead>>>();
            var dataResult = ((SuccessResult<IEnumerable<SosAllocHead>>)this.results).Data;
            dataResult.Count().Should().Be(2);
        }
    }
}