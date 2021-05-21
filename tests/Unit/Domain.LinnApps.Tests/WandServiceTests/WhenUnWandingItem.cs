namespace Linn.Stores.Domain.LinnApps.Tests.WandServiceTests
{
    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Wand;
    using Linn.Stores.Domain.LinnApps.Wand.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUnWandingItem : ContextBase
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
            this.wandAction = "U";
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
                                   TransType = "U"
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
            this.wandPackResult = new WandPackResult { Message = "ok", Success = true, WandLogId = this.wandLogId };
            this.WandPack.Wand(this.wandAction, this.userNumber, this.consignmentId, this.wandString)
                .Returns(this.wandPackResult);

            this.result = this.Sut.Wand(this.wandAction, this.wandString, this.consignmentId, this.userNumber);
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
        public void ShouldNotGetConsignment()
        {
            this.ConsignmentRepository.DidNotReceive().FindById(Arg.Any<int>());
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
