﻿namespace Linn.Stores.Domain.LinnApps.Tests.PartServiceTests
{
    using System;
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Parts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdatingPartToBeQc : ContextBase
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
                              RawOrFinished = "R",
                              QcOnReceipt = "Y",
                              QcInformation = "Making it QC"
                          };
            this.privileges = new List<string> { "part.admin" };
            this.PartPack.PartLiveTest(Arg.Any<string>(), out _).Returns(true);
            this.AuthService.HasPermissionFor(AuthorisedAction.PartAdmin, this.privileges).Returns(true);
            this.Sut.UpdatePart(this.from, this.to, this.privileges, 33087);
        }

        [Test]
        public void ShouldUpdate()
        {
            this.from.QcOnReceipt.Should().Be(this.to.QcOnReceipt);
            this.from.QcInformation.Should().Be(this.to.QcInformation);
        }

        [Test]
        public void ShouldAddQcControlRecord()
        {
            this.QcControlRepo.Received().Add(Arg.Is<QcControl>(x => 
                x.PartNumber == this.to.PartNumber
                && x.OnOrOffQc == "ON"
                && x.ChangedBy == 33087
                && x.TransactionDate == DateTime.Today.Date
                && x.Reason == this.to.QcInformation));
        }
    }
}
