namespace Linn.Stores.Domain.LinnApps.Tests.PartServiceTests
{
    using System;
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.Parts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenTakingPartOffQcAndNoReasonGiven : ContextBase
    {
        private Part to;

        private Part from;

        private List<string> privileges;

        private Action act;

        [SetUp]
        public void SetUp()
        {
            this.from = new Part { QcOnReceipt = "Y", QcInformation = "NEW SUPPLIER" };
            this.to = new Part
                          {
                              PartNumber = "PART",
                              RawOrFinished = "R",
                              QcOnReceipt = "N",
                              QcInformation = string.Empty
                          };
            this.privileges = new List<string> { "part.admin", "part.qc-controller" };
            this.PartPack.PartLiveTest(Arg.Any<string>(), out _).Returns(true);
            this.AuthService.HasPermissionFor(AuthorisedAction.PartAdmin, this.privileges).Returns(true);
            this.AuthService.HasPermissionFor(AuthorisedAction.PartQcController, this.privileges).Returns(true);

            this.act = () => this.Sut.UpdatePart(this.from, this.to, this.privileges, 33087);
        }

        [Test]
        public void ShouldNotUpdate()
        {
            this.from.QcOnReceipt.Should().Be("Y");
            this.from.QcInformation.Should().Be("NEW SUPPLIER");
        }

        [Test]
        public void ShouldThrow()
        {
            this.act.Should().Throw<UpdatePartException>()
                .WithMessage("Must specify a reason (QC Information) if setting part to be off QC.");
        }
    }
}
