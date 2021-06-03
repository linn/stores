﻿namespace Linn.Stores.Domain.LinnApps.Tests.ConsignmentShipfileServiceTests
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.ConsignmentShipfiles;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenSendingAnEmailAndEmailAlreadySent : ContextBase
    {
        private IEnumerable<ConsignmentShipfile> toSend;

        private IEnumerable<ConsignmentShipfile> result;

       [SetUp]
        public void SetUp()
        {
            this.toSend = new List<ConsignmentShipfile>
                              {
                                  new ConsignmentShipfile
                                      {
                                          Id = 1
                                      }
                              };

            this.ShipfileRepository.FindById(1).Returns(
                new ConsignmentShipfile { ShipfileSent = "Y", Message = ShipfileStatusMessages.EmailSent });

            this.result = this.Sut.SendEmails(this.toSend);
        }

        [Test]
        public void ShouldNotSendEmail()
        {
            this.EmailService.DidNotReceive().SendEmail(
                Arg.Any<string>(),
                Arg.Any<string>(),
                null, 
                null,
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<Stream>());
        }

        [Test]
        public void ShouldUpdateStatusMessage()
        {
            this.result.First().Message.Should().Be(ShipfileStatusMessages.EmailAlreadySent);
        }
    }
}
