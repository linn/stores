﻿namespace Linn.Stores.Domain.LinnApps.Tests.WandServiceTests
{
    using System;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Wand;
    using Linn.Stores.Domain.LinnApps.Wand.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenWandingItemWithAddressLabel : ContextBase
    {
        private int consignmentId;

        private WandResult result;

        private string wandString;

        private string wandAction;

        private WandPackResult wandPackResult;

        private int userNumber;

        private int wandLogId;

        private WandLog wandLog;

        private Consignment consignment;

        private bool printLabels;

        [SetUp]
        public void SetUp()
        {
            this.wandAction = "W";
            this.printLabels = true;
            this.consignmentId = 134;
            this.wandString = "flajdlfjd1312";
            this.userNumber = 35345;
            this.wandLogId = 123;
            this.wandLog = new WandLog
                               {
                                   Id = this.wandLogId,
                                   ArticleNumber = "a",
                                   ConsignmentId = this.consignmentId,
                                   ContainerNo = 1,
                                   TransType = "W"
                               };
            this.consignment = new Consignment
                                   {
                                       ConsignmentId = this.consignmentId,
                                       Address = new Address
                                                     {
                                                         Line1 = "this",
                                                         Line2 = "address",
                                                         PostCode = "d",
                                                         CountryCode = "FR",
                                                         Country = new Country { CountryCode = "FR", DisplayName = "France" }
                                                     }
                                   };
            this.WandLogRepository.FindById(this.wandLogId).Returns(this.wandLog);
            this.ConsignmentRepository.FindById(this.consignmentId).Returns(this.consignment);
            this.wandPackResult = new WandPackResult { Message = "ok", Success = true, WandLogId = this.wandLogId };
            this.WandPack.Wand(this.wandAction, this.userNumber, this.consignmentId, this.wandString)
                .Returns(this.wandPackResult);
            this.PrinterMappingRepository.FindBy(Arg.Any<Expression<Func<PrinterMapping, bool>>>()).Returns(
                new PrinterMapping
                    {
                        UserNumber = this.userNumber, PrinterGroup = "DISPATCH-LABEL", PrinterName = "DispatchLabels1"
                    });
            this.result = this.Sut.Wand(
                this.wandAction,
                this.wandString,
                this.consignmentId,
                this.userNumber,
                this.printLabels);
        }

        [Test]
        public void ShouldGetWandLog()
        {
            this.WandLogRepository.Received().FindById(this.wandLogId);
        }

        [Test]
        public void ShouldCallProxy()
        {
            this.WandPack.Received().Wand(this.wandAction, this.userNumber, this.consignmentId, this.wandString);
        }

        [Test]
        public void ShouldGetConsignment()
        {
            this.ConsignmentRepository.Received().FindById(this.consignmentId);
        }

        [Test]
        public void ShouldPrintLabel()
        {
            this.BartenderLabelPack.Received().PrintLabels(
                $"Address{this.wandLogId}",
                "DispatchLabels1",
                1,
                "dispatchaddress.btw",
                Arg.Any<string>(),
                ref Arg.Any<string>());
        }

        [Test]
        public void ShouldReturnResult()
        {
            this.result.Message.Should().Be(this.wandPackResult.Message);
            this.result.Success.Should().BeTrue();
        }
    }
}
