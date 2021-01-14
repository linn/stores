namespace Linn.Stores.Domain.LinnApps.Tests.WorkstationServiceTests
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Stores.Domain.LinnApps.ProductionTriggers;
    using Linn.Stores.Domain.LinnApps.Workstation;
    using Linn.Stores.Domain.LinnApps.Workstation.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenStartingTopUpRun : ContextBase
    {
        private WorkstationTopUpStatus result;

        private List<TopUpListJobRef> topUpList;

        private PtlMaster ptlMaster;

        [SetUp]
        public void SetUp()
        {
            this.ptlMaster = new PtlMaster
                                 {
                                     LastFullJobRef = "G",
                                     LastFullRunDate = 1.December(2022).AddHours(1),
                                     LastFullRunMinutesTaken = 5
                                 };
            this.PtlRepository.GetRecord()
                .Returns(this.ptlMaster);
            this.topUpList = new List<TopUpListJobRef>
                                 {
                                     new TopUpListJobRef { JobRef = "F", DateRun = 1.December(2022).AddHours(1) }
                                 };
            this.TopUpListJobRefRepository.FindAll().Returns(this.topUpList.AsQueryable());

            this.result = this.Sut.StartTopUpRun();
        }

        [Test]
        public void ShouldCallProxy()
        {
            this.WorkstationPack.Received().StartTopUpRun();
        }

        [Test]
        public void ShouldReturnStatus()
        {
            this.result.ProductionTriggerRunJobRef.Should().Be(this.ptlMaster.LastFullJobRef);
            this.result.WorkstationTopUpJobRef.Should().Be("F");
            this.result.StatusMessage.Should().Be("Workstation top up run has been started");
        }
    }
}
