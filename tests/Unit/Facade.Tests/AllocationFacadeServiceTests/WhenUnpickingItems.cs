namespace Linn.Stores.Facade.Tests.AllocationFacadeServiceTests
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Allocation;
    using Linn.Stores.Resources.RequestResources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUnpickingItems : ContextBase
    {
        private IEnumerable<SosAllocDetail> items;

        private AccountOutletRequestResource resource;

        private IResult<IEnumerable<SosAllocDetail>> results;

        [SetUp]
        public void SetUp()
        {
            this.resource = new AccountOutletRequestResource { JobId = 1, AccountId = 2, OutletNumber = 3 };
            this.items = new List<SosAllocDetail> { new SosAllocDetail { Id = 303 } };

            this.AllocationService.UnpickItems(1, 2, 3)
                .Returns(this.items);

            this.results = this.Sut.UnpickItems(this.resource);
        }

        [Test]
        public void ShouldCallService()
        {
            this.AllocationService.Received().UnpickItems(1, 2, 3);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.results.Should().BeOfType<SuccessResult<IEnumerable<SosAllocDetail>>>();
            var dataResult = ((SuccessResult<IEnumerable<SosAllocDetail>>)this.results).Data;
            dataResult.First().Id.Should().Be(this.items.First().Id);
        }
    }
}
