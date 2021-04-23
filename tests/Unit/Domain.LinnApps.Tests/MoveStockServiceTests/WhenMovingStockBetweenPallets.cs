namespace Linn.Stores.Domain.LinnApps.Tests.MoveStockServiceTests
{
    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Domain.LinnApps.StockMove.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenMovingStockBetweenPallets : ContextBase
    {
        private RequisitionProcessResult result;

        [SetUp]
        public void SetUp()
        {
            this.From = "P1000";
            this.To = "P2000";

            this.StoresPack.CheckStockAtFromLocation(
                    this.PartNumber,
                    this.Quantity,
                    this.From,
                    null,
                    1000,
                    this.FromStockDate)
                .Returns(new RequisitionProcessResult(true, "ok"));
            this.StoresPack.MoveStock(
                this.ReqNumber,
                3,
                this.PartNumber,
                this.Quantity,
                null,
                1000,
                this.FromStockDate,
                null,
                2000,
                null,
                null,
                null).Returns(new ProcessResult(true, "ok"));
            this.result = this.Sut.MoveStock(
                this.ReqNumber,
                this.PartNumber,
                this.Quantity,
                this.From,
                null,
                null,
                this.FromStockDate,
                null,
                null,
                this.To,
                null,
                null,
                null,
                null,
                this.UserNumber);
        }

        [Test]
        public void ShouldNotMakeNewReq()
        {
            this.StoresPack.DidNotReceive().CreateMoveReq(Arg.Any<int>());
        }

        [Test]
        public void ShouldCheckFromLocationStock()
        {
            this.StoresPack.Received().CheckStockAtFromLocation(
                this.PartNumber,
                this.Quantity,
                this.From,
                null,
                1000,
                this.FromStockDate);
        }

        [Test]
        public void ShouldDoMove()
        {
            this.StoresPack.Received().MoveStock(
                this.ReqNumber,
                3,
                this.PartNumber,
                this.Quantity,
                null,
                1000,
                this.FromStockDate,
                null,
                2000,
                null,
                null,
                null);
        }

        [Test]
        public void ShouldReturnResult()
        {
            this.result.ReqNumber.Should().Be(this.ReqNumber);
            this.result.Success.Should().BeTrue();
            this.result.Message.Should().Be("ok");
        }
    }
}
