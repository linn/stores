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

    public class WhenSendingEmailToAnOrgWithMultipleOutletsWithTheSameEmailAddress : ContextBase
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
                ContactDetails = new Contact { EmailAddress = "outlet@linn.co.uk", AddressId = 1 }
            };
            var orders = new List<SalesOrder>
                              {
                                  new SalesOrder
                                      {
                                          SalesOutlet = new SalesOutlet
                                                            {
                                                                OrderContact = new Contact
                                                                                   {
                                                                                       EmailAddress = "outlet@linn.co.uk"
                                                                                   }
                                                            }
                                      },
                                  new SalesOrder
                                      {
                                          SalesOutlet = new SalesOutlet
                                                            {
                                                                OrderContact = new Contact
                                                                                   {
                                                                                       EmailAddress = "outlet@linn.co.uk"
                                                                                   }
                                                            }
                                      }
                              };
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
                              

            this.PackingListService.BuildPackingList(Arg.Any<IEnumerable<PackingListItem>>())
                .ReturnsForAnyArgs(new List<PackingListItem> { new PackingListItem(1, 1, "desc", 1m) });

            this.ShipfileRepository.FindById(1).Returns(this.shipfileData);
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
        public void ShouldSendOneEmail()
        {
            var correctBody =
                $"Please find attached the following documents for your information {System.Environment.NewLine}{System.Environment.NewLine}";
            correctBody +=
                $"Packing List {System.Environment.NewLine}";
            correctBody +=
                $"Despatch Note/Serial Number List {System.Environment.NewLine}";
            correctBody +=
                $"These refer to goods that left the factory on 12/05/2008 09:34:58 {System.Environment.NewLine}";
            correctBody +=
                $"The shipment should arrive tomorrow.";
            this.EmailService.Received(1).SendEmail(
                "outlet@linn.co.uk",
                "outlet@linn.co.uk",
                null,
                null,
                Arg.Any<string>(),
                Arg.Any<string>(),
                "Shipping Details",
                correctBody,
                Arg.Any<Stream>(),
                Arg.Any<string>());
        }
        [Test]
        public void ShouldUpdateStatusMessage()
        {
            this.result.Message.Should().Be(ShipfileStatusMessages.EmailSent);
            this.result.ShipfileSent.Should().Be("Y");
        }
    }
}
