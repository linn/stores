namespace Linn.Stores.Domain.LinnApps.Tests.AllocationServiceTests
{
    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Allocation.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenFinishingAllocation : ContextBase
    {
        private const int JobId = 321;

        private AllocationResult result;

        [SetUp]
        public void SetUp()
        {
            this.AllocPack.When(a => a.FinishAllocation(JobId, out Arg.Any<string>(), out Arg.Any<string>()))
                .Do(x => { x[2] = "Y"; });
            this.result = this.Sut.FinishAllocation(JobId);
        }

        [Test]
        public void ShouldCallProxy()
        {
            this.AllocPack.Received().FinishAllocation(JobId, out Arg.Any<string>(), out Arg.Any<string>());
        }

        [Test]
        public void ShouldReturnStartDetails()
        {
            this.result.Id.Should().Be(JobId);
        }
    }
}
