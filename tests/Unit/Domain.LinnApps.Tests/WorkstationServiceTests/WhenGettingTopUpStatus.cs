namespace Linn.Stores.Domain.LinnApps.Tests.WorkstationServiceTests
{
    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.ProductionTriggers;
    using Linn.Stores.Domain.LinnApps.Workstation.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingTopUpStatus : ContextBase
    {
        private WorkstationTopUpStatus result;

        [SetUp]
        public void SetUp()
        {
            this.PtlRepository.GetRecord()
                .Returns(new PtlMaster { LastFullJobRef = "a" });
            this.result = this.Sut.GetTopUpStatus();
        }

        [Test]
        public void ShouldReturnStatus()
        {
            this.result.ProductionTriggerRunJobRef.Should().Be("a");
        }
    }
}
