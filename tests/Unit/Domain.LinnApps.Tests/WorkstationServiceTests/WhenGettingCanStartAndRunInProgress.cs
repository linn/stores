namespace Linn.Stores.Domain.LinnApps.Tests.WorkstationServiceTests
{
    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Workstation.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingCanStartAndRunInProgress : ContextBase
    {
        private bool result;

        private WorkstationTopUpStatus status;

        [SetUp]
        public void SetUp()
        {
            this.status = new WorkstationTopUpStatus { ProductionTriggerRunJobRef = "a", WorkstationTopUpJobRef = "b" };
            this.WorkstationPack.TopUpRunProgressStatus().Returns("in progress");
            this.result = this.Sut.CanStartNewRun(this.status);
        }
        
        [Test]
        public void ShouldReturnFalse()
        {
            this.result.Should().BeFalse();
        }
    }
}
