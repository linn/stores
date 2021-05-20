﻿namespace Linn.Stores.Domain.LinnApps.Tests.ConsignmentShipfileServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Models.Emails;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenSendingEmailToAnOrg : ContextBase
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

            this.DataService.BuildPdfModel(Arg.Any<int>(), Arg.Any<int>()).Returns(
                new ConsignmentShipfilePdfModel
                {
                    PackingList = new PackingListItem[] { new PackingListItem() },
                    DespatchNotes = new DespatchNote[] { new DespatchNote() },
                    DateDispatched = "12/05/2008 09:34:58"
                });

            this.result = this.Sut.SendEmails(this.toSend);
        }

        [Test]
        public void ShouldSendEmail()
        {
            var correctBody =
                $"Please find attached the following documents for your information {System.Environment.NewLine}{System.Environment.NewLine}";
            correctBody += $"Packing List {System.Environment.NewLine}";
            correctBody += $"Despatch Note/Serial Number List {System.Environment.NewLine}";
            correctBody += $"These refer to goods that left the factory on 12/05/2008 09:34:58 {System.Environment.NewLine}";

            this.EmailService.Received().SendEmail(
                "outlet@linn.co.uk",
                "outlet@linn.co.uk",
                null,
                null,
                Arg.Any<string>(),
                Arg.Any<string>(),
                "Shipping Details",
                correctBody,
                Arg.Any<Stream>());
        }

        [Test]
        public void ShouldUpdateStatusMessage()
        {
            this.result.First().Message.Should().Be(ShipfileStatusMessages.EmailSent);
            this.result.First().ShipfileSent.Should().Be("Y");
        }
    }
}