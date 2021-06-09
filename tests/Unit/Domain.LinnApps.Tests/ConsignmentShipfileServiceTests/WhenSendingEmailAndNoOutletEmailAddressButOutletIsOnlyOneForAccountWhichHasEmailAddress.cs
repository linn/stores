namespace Linn.Stores.Domain.LinnApps.Tests.ConsignmentShipfileServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Domain.LinnApps.ConsignmentShipfiles;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenSendingEmailAndNoOutletEmailAddressButOutletIsOnlyOneForAccountWhichHasEmailAddress : ContextBase
    {
        private ConsignmentShipfile toSend;

        private ConsignmentShipfile result;

        private ConsignmentShipfile shipfileData;

        [SetUp]
        public void SetUp()
        {
            var account = new SalesAccount
            {
                OrgId = 1,
                ContactId = 1,
                ContactDetails = new Contact { EmailAddress = "account@linn.co.uk", AddressId = 1 }
            };

            var orders = new List<SalesOrder>
                              {
                                  new SalesOrder
                                      {
                                          SalesOutlet = new SalesOutlet
                                                            {
                                                                AccountId = 1,
                                                                OutletNumber = 1,
                                                                OrderContact = new Contact
                                                                                   {
                                                                                       EmailAddress = null,
                                                                                       AddressId = 1
                                                                                   }
                                                            }
                                      },
                              };
            var outlet = new SalesOutlet { OutletAddressId = 1 };
            var outlets = new List<SalesOutlet> { outlet };

            var consignment = new Consignment
            {
                SalesAccount = account,
                Items = new List<ConsignmentItem> { new ConsignmentItem { OrderNumber = 1 } },
                Address = new Address { Country = new Country { CountryCode = "GB" } }
            };

            this.shipfileData = new ConsignmentShipfile
            {
                Id = 1,
                Consignment = consignment
            };

            this.toSend = new ConsignmentShipfile { Id = 1 };

            this.ShipfileRepository.FindById(1).Returns(this.shipfileData);

            this.OutletRepository.FilterBy(Arg.Any<Expression<Func<SalesOutlet, bool>>>())
                .Returns(outlets.AsQueryable());

            this.OutletRepository.FindBy(Arg.Any<Expression<Func<SalesOutlet, bool>>>()).Returns(outlet);

            this.SalesOrderRepository.FilterBy(Arg.Any<Expression<Func<SalesOrder, bool>>>()).Returns(orders.AsQueryable());

            this.ConsignmentRepository.FindById(1).Returns(consignment);

            this.DataService.GetPdfModelData(Arg.Any<int>(), Arg.Any<int>()).Returns(
                new ConsignmentShipfilePdfModel
                {
                    PackingList = new PackingListItem[] { },
                    DespatchNotes = new DespatchNote[] { new DespatchNote() },
                    DateDispatched = "12/05/2008 09:34:58",
                    ConsignmentNumber = "1"
                });

            this.result = this.Sut.SendEmails(this.toSend);
        }

        [Test]
        public void ShouldSendEmailToAccount()
        {
            this.EmailService.Received().SendEmail(
                "account@linn.co.uk",
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
            this.result.Message.Should().Be(ShipfileStatusMessages.EmailSent);
        }
    }
}
