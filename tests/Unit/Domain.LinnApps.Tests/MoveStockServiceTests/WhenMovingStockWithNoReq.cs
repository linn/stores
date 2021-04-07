namespace Linn.Stores.Domain.LinnApps.Tests.MoveStockServiceTests
{
    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Domain.LinnApps.StockMove.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenMovingStockWithNoReq : ContextBase
    {
        private RequisitionProcessResult result;

        private RequisitionProcessResult storesPackResult;
        
        private int? existingReqNumber;

        [SetUp]
        public void SetUp()
        {
            this.existingReqNumber = null;
            this.storesPackResult =
                new RequisitionProcessResult { Success = true, Message = "ok", ReqNumber = this.ReqNumber };
            this.From = "P1000";
            this.To = "P2000";

            this.StoresPack.CreateMoveReq(this.UserNumber)
                .Returns(this.storesPackResult);
            this.StoresPack.CheckStockAtFromLocation(this.PartNumber, this.Quantity, this.From, null, 1000, null)
                .Returns(new RequisitionProcessResult(true, "ok"));
            this.StoresPack.MoveStock(
                this.ReqNumber,
                3,
                this.PartNumber,
                this.Quantity,
                null,
                1000,
                null,
                null,
                2000,
                null,
                null,
                null).Returns(new ProcessResult(true, "ok"));
            this.result = this.Sut.MoveStock(
                this.existingReqNumber,
                this.PartNumber,
                this.Quantity,
                this.From,
                null,
                null,
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
