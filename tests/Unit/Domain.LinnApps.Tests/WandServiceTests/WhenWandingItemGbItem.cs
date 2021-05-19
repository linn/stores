namespace Linn.Stores.Domain.LinnApps.Tests.WandServiceTests
{
    using System;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Wand;
    using Linn.Stores.Domain.LinnApps.Wand.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenWandingItemGbItem : ContextBase
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

        [SetUp]
        public void SetUp()
        {
            this.wandAction = "W";
            this.consignmentId = 134;
            this.wandString = "flajdlfjd1312";
            this.userNumber = 35345;
            this.wandLogId = 123;
            this.wandLog = new WandLog { Id = this.wandLogId, ArticleNumber = "a", ConsignmentId = 123, ContainerNo = 1, TransType = "W" };
            this.consignment = new Consignment
                                   {
                                       ConsignmentId = 123,
                                       Address = new Address
                                                     {
                                                         Line1 = "this",
                                                         Line2 = "address",
                                                         PostCode = "d",
                                                         CountryCode = "GB",
                                                         Country = new Country { CountryCode = "GB", DisplayName = "GB" }
                                                     }
                                   };
            this.WandLogRepository.FindById(this.wandLogId).Returns(this.wandLog);
            this.ConsignmentRepository.FindBy(Arg.Any<Expression<Func<Consignment, bool>>>()).Returns(this.consignment);
            this.wandPackResult = new WandPackResult { Message = "ok", Success = true, WandLogId = this.wandLogId };
            this.WandPack.Wand(this.wandAction, this.userNumber, this.consignmentId, this.wandString)
                .Returns(this.wandPackResult);

            this.result = this.Sut.Wand(this.wandAction, this.wandString, this.consignmentId, this.userNumber);
        }

        [Test]
        public void ShouldGetConsignment()
        {
            this.ConsignmentRepository.Received().FindBy(Arg.Any<Expression<Func<Consignment, bool>>>());
        }

        [Test]
        public void ShouldNotPrintLabel()
        {
            this.BartenderLabelPack.DidNotReceive().PrintLabels(
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<int>(),
                Arg.Any<string>(),
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
