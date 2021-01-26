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

    public class WhenGettingTopUpStatus : ContextBase
    {
        private WorkstationTopUpStatus result;

        private List<TopUpListJobRef> topUpList;

        private PtlMaster ptlMaster;

        private string progressMessage;

        [SetUp]
        public void SetUp()
        {
            this.progressMessage = "in progress";
            this.ptlMaster = new PtlMaster
                                 {
                                     LastFullJobRef = "G",
                                     LastFullRunDate = 1.December(2022).AddHours(1),
                                     LastFullRunMinutesTaken = 5
                                 };
            this.PtlRepository.GetRecord()
                .Returns(this.ptlMaster);
            this.WorkstationPack.TopUpRunProgressStatus()
                .Returns(this.progressMessage);
            this.topUpList = new List<TopUpListJobRef>
                                 {
                                     new TopUpListJobRef { JobRef = "F", DateRun = 1.December(2022).AddHours(1) },
                                     new TopUpListJobRef { JobRef = "G", DateRun = 1.December(2022).AddHours(2) }
                                 };
            this.TopUpListJobRefRepository.FindAll().Returns(this.topUpList.AsQueryable());
            this.result = this.Sut.GetTopUpStatus();
        }

        [Test]
        public void ShouldCallTriggerRepository()
        {
            this.PtlRepository.Received().GetRecord();
        }

        [Test]
        public void ShouldCallTopUpRepository()
        {
            this.TopUpListJobRefRepository.Received().FindAll();
        }

        [Test]
        public void ShouldGetInProgressMessage()
        {
            this.WorkstationPack.Received().TopUpRunProgressStatus();
        }

        [Test]
        public void ShouldReturnStatus()
        {
            this.result.ProductionTriggerRunJobRef.Should().Be(this.ptlMaster.LastFullJobRef);
            this.result.WorkstationTopUpJobRef.Should().Be("G");
            this.result.ProductionTriggerRunMessage.Should().Be("The last run was on 01-Dec-2022 at 1:00 AM and took 5 minutes.");
            this.result.WorkstationTopUpMessage.Should().Be("The last run was on 01-Dec-2022 at 2:00 AM.");
            this.result.StatusMessage.Should().Be(this.progressMessage);
        }
    }
}
