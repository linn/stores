namespace Linn.Stores.Facade.Tests.SosAllocHeadFacadeServiceTests
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Allocation;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingAllAllocHeads : ContextBase
    {
        private IResult<IEnumerable<SosAllocHead>> results;

        [SetUp]
        public void SetUp()
        {
            this.SosAllocHeadRepository.FindAll()
                .Returns(new List<SosAllocHead> { new SosAllocHead(), new SosAllocHead() }.AsQueryable());
            this.results = this.Sut.GetAllAllocHeads();
        }

        [Test]
        public void ShouldCallRepository()
        {
            this.SosAllocHeadRepository.Received().FindAll();
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