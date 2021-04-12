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

    public class WhenGettingCanStartWithoutStatus : ContextBase
    {
        private bool result;

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
            this.WorkstationPack.TopUpRunProgressStatus().Returns(string.Empty);

            this.result = this.Sut.CanStartNewRun();
        }

        [Test]
        public void ShouldReturnStatus()
        {
            this.result.Should().BeTrue();
        }
    }
}
