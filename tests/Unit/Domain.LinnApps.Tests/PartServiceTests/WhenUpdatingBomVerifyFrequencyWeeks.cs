namespace Linn.Stores.Domain.LinnApps.Tests.PartServiceTests
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Parts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdatingBomVerifyFrequencyWeeks : ContextBase
    {
        private Part to;

        private Part from;

        private List<string> privileges;

        [SetUp]
        public void SetUp()
        {
            this.from = new Part
            {
                PartNumber = "A-PART-LIKE-NO-OTHER",
                BomVerifyFreqWeeks = 0,
                RawOrFinished = "F",
                QcOnReceipt = "N"
            };
            this.to = new Part
            {
                PartNumber = "A-PART-LIKE-NO-OTHER",
                BomVerifyFreqWeeks = 600,
                RawOrFinished = "F",
                QcOnReceipt = "N"
            };
            this.privileges = new List<string> { "part.admin" };
            this.PartPack.PartLiveTest(Arg.Any<string>(), out _).Returns(true);
            this.AuthService.HasPermissionFor(AuthorisedAction.PartAdmin, this.privileges).Returns(true);
            this.Sut.UpdatePart(this.from, this.to, this.privileges, 33087);
        }

        [Test]
        public void ShouldSetBomVerifyFrequencyWeeks()
        {
            this.from.PartNumber.Should().Be("A-PART-LIKE-NO-OTHER");
            this.from.BomVerifyFreqWeeks.Should().Be(600);
        }
    }
}