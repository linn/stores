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

    public class WhenSendingEmailsToAnOrgAndContactDetailsMissingForAnOutlet : ContextBase
    {
        private IEnumerable<ConsignmentShipfile> toSend;

        private IEnumerable<ConsignmentShipfile> result;

        private ConsignmentShipfile shipfileData;

        [SetUp]
        public void SetUp()
        {
            var account = new SalesAccount
            {
                OrgId = 1,
                ContactId = 1,
                ContactDetails = new Contact { EmailAddress = "customer@linn.co.uk", AddressId = 1 }
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
                                                                                       EmailAddress = null
                                                                                   }
                                                            }
                                      },
                                  new SalesOrder
                                      {
                                          SalesOutlet = new SalesOutlet
                                                            {
                                                                AccountId = 1,
                                                                OutletNumber = 2,
                                                                OrderContact = new Contact
                                                                                   {
                                                                                       EmailAddress = null
                                                                                   }
                                                            }
                                      }
                              };
            var consignment = new Consignment
            {
                SalesAccount = account,
                Items = new List<ConsignmentItem> { new ConsignmentItem { OrderNumber = 1 } }
            };
            this.shipfileData = new ConsignmentShipfile
            {
                Id = 1,
                Consignment = consignment
            };

            this.toSend = new List<ConsignmentShipfile>
                              {
                                  new ConsignmentShipfile { Id = 1 }
                              };

            this.ShipfileRepository.FindById(1).Returns(this.shipfileData);

            this.SalesOrderRepository.FilterBy(Arg.Any<Expression<Func<SalesOrder, bool>>>()).Returns(orders.AsQueryable());
            this.DataService.GetPdfModelData(Arg.Any<int>(), Arg.Any<int>()).Returns(
                new ConsignmentShipfilePdfModel
                {
                    PackingList = new PackingListItem[] { },
                    DespatchNotes = new DespatchNote[] { new DespatchNote() },
                    DateDispatched = "12/05/2008 09:34:58"
                });
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
            this.result.First().Message.Should().Be(ShipfileStatusMessages.NoContactDetailsForAnOutlet);
        }
    }
}
