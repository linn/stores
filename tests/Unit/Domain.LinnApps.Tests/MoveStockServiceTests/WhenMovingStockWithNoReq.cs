namespace Linn.Stores.Domain.LinnApps.Tests.MoveStockServiceTests
{
    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.StockMove.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenMovingStockWithNoReq : ContextBase
    {
        private RequisitionProcessResult result;

        private RequisitionProcessResult storesPackResult;

        [SetUp]
        public void SetUp()
        {
            this.ReqNumber = null;
            this.storesPackResult = new RequisitionProcessResult { Success = true, Message = "ok", ReqNumber = 34342 };
            this.From = "P1000";
            this.To = "P2000";

            this.StoresPack.CreateMoveReq(this.UserNumber)
                .Returns(this.storesPackResult);

            this.result = this.Sut.MoveStock(
                this.ReqNumber,
                this.PartNumber,
                this.Quantity,
                this.From,
                null,
                null,
                null,
                this.To,
                null,
                null,
                null,
                this.UserNumber);
        }

        [Test]
        public void ShouldMakeReq()
        {
            this.StoresPack.Received().CreateMoveReq(this.UserNumber);
        }

        [Test]
        public void ShouldReturnResult()
        {
            this.result.ReqNumber.Should().Be(this.storesPackResult.ReqNumber);
        }
    }
}
