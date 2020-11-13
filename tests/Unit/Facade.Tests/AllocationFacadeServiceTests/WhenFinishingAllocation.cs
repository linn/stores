namespace Linn.Stores.Facade.Tests.AllocationFacadeServiceTests
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Allocation.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenFinishingAllocation : ContextBase
    {
        private AllocationResult allocationResult;

        private IResult<AllocationResult> result;

        private int jobId;

        [SetUp]
        public void SetUp()
        {
            this.jobId = 1234;
            this.allocationResult = new AllocationResult(this.jobId);

            this.AllocationService.FinishAllocation(this.jobId)
                .Returns(this.allocationResult);

            this.result = this.Sut.FinishAllocation(this.jobId);
        }

        [Test]
        public void ShouldCallService()
        {
            this.AllocationService.Received().FinishAllocation(this.jobId);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<AllocationResult>>();
            var dataResult = ((SuccessResult<AllocationResult>)this.result).Data;
            dataResult.Id.Should().Be(this.allocationResult.Id);
        }
    }
}
