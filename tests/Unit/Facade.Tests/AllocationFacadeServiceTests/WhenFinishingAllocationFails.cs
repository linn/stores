namespace Linn.Stores.Facade.Tests.AllocationFacadeServiceTests
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Allocation.Models;
    using Linn.Stores.Domain.LinnApps.Exceptions;

    using NSubstitute;
    using NSubstitute.ExceptionExtensions;

    using NUnit.Framework;

    public class WhenFinishingAllocationFails : ContextBase
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
                .Throws(new FinishAllocationException("alarm"));

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
            this.result.Should().BeOfType<BadRequestResult<AllocationResult>>();
            var message = ((BadRequestResult<AllocationResult>)this.result).Message;
            message.Should().Be("alarm");
        }
    }
}
