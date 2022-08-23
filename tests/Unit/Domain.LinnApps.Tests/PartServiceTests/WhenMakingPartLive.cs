namespace Linn.Stores.Domain.LinnApps.Tests.PartServiceTests
{
    using System;
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Parts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenMakingPartLive : ContextBase
    {
        private Part to;

        private Part from;

        private List<string> privileges;

        [SetUp]
        public void SetUp()
        {
            this.from = new Part();
            this.to = new Part
                          {
                              PartNumber = "PART",
                              MadeLiveBy = new Employee { Id = 1 },
                              DateLive = DateTime.UnixEpoch,
                              RawOrFinished = "R",
                              QcOnReceipt = "N"
                          };
            this.privileges = new List<string> { "part.admin" };
            this.PartPack.PartLiveTest(Arg.Any<string>(), out _).Returns(true);
            this.AuthService.HasPermissionFor(AuthorisedAction.PartAdmin, this.privileges).Returns(true);
            this.Sut.UpdatePart(this.from, this.to, this.privileges);
        }

        [Test]
        public void ShouldSetDatePartLive()
        {
            this.from.DateLive.Should().Be(DateTime.UnixEpoch);
        }

        [Test]
        public void ShouldSetLiveBy()
        {
            this.from.DateLive.Should().Be(DateTime.UnixEpoch);
            this.from.MadeLiveBy.Id.Should().Be(1);
        }
    }
}