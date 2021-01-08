namespace Linn.Stores.Domain.LinnApps.Tests.AllocationServiceTests
{
    using System;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Exceptions;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenFinishingAllocationFails : ContextBase
    {
        private const int JobId = 321;

        private Action action;

        [SetUp]
        public void SetUp()
        {
            this.AllocPack.When(a => a.FinishAllocation(JobId, out Arg.Any<string>(), out Arg.Any<string>()))
                .Do(x => { x[2] = "N"; });
            this.action = () => this.Sut.FinishAllocation(JobId);
        }

        [Test]
        public void ShouldThrowException()
        {
            this.action.Should().Throw<FinishAllocationException>();
        }
    }
}
