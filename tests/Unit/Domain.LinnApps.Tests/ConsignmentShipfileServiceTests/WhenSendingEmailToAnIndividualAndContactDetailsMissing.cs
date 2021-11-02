namespace Linn.Stores.Domain.LinnApps.Tests.ConsignmentShipfileServiceTests
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Domain.LinnApps.ConsignmentShipfiles;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenSendingEmailToAnIndividualAndContactDetailsMissing : ContextBase
    {
        private ConsignmentShipfile toSend;

        private ConsignmentShipfile result;

        private ConsignmentShipfile shipfileData;

        [SetUp]
        public void SetUp()
        {
            this.shipfileData = new ConsignmentShipfile
            {
                Id = 1,
                Consignment = new Consignment
                {
                    SalesAccount = new SalesAccount
                                       {
                                           OrgId = null,
                                           ContactDetails = new Contact()
                                       }
                }
            };
            this.toSend = new ConsignmentShipfile
                                      {
                                          Id = 1,
                                      };

            this.ShipfileRepository.FindById(1).Returns(this.shipfileData);

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
                Arg.Any<Stream>(),
                Arg.Any<string>());
        }
        [Test]
        public void ShouldUpdateStatusMessage()
        {
            this.result.Message.Should().Be(ShipfileStatusMessages.NoContactDetails);
        }
    }
}
