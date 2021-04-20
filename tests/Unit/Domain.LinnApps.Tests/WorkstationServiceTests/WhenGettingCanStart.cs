namespace Linn.Stores.Domain.LinnApps.Tests.WorkstationServiceTests
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Stores.Domain.LinnApps.ProductionTriggers;
    using Linn.Stores.Domain.LinnApps.Workstation;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingCanStart : ContextBase
    {
        private bool result;

        private PtlMaster ptlMaster;

        private List<TopUpListJobRef> topUpList;

        private string progressMessage;

        [SetUp]
        public void SetUp()
        {
            this.progressMessage = string.Empty;
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
                                     new TopUpListJobRef { JobRef = "F", DateRun = 1.December(2022).AddHours(1) }
                                 };
            this.TopUpListJobRefRepository.FindAll().Returns(this.topUpList.AsQueryable());
            this.result = this.Sut.CanStartNewRun();
        }
        
        [Test]
        public void ShouldReturnTrue()
        {
            this.result.Should().BeTrue();
        }
    }
}
