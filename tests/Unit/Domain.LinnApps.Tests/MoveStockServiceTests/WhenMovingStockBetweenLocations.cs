namespace Linn.Stores.Domain.LinnApps.Tests.MoveStockServiceTests
{
    using System;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Domain.LinnApps.StockMove.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenMovingStockBetweenLocations : ContextBase
    {
        private RequisitionProcessResult result;

        private int fromLocationId;

        private int toLocationId;

        [SetUp]
        public void SetUp()
        {
            this.fromLocationId = 23;
            this.toLocationId = 34;
            this.From = "E-LOC-1";
            this.To = "E-LOC-2";

            this.StoresPack.CheckStockAtFromLocation(
                    this.PartNumber,
                    this.Quantity,
                    this.From,
                    this.fromLocationId,
                    null,
                    this.FromStockDate)
                .Returns(new RequisitionProcessResult(true, "ok"));

            this.StorageLocationRepository.FindBy(Arg.Any<Expression<Func<StorageLocation, bool>>>())
                .Returns(
                    new StorageLocation { LocationId = this.fromLocationId },
                    new StorageLocation { LocationId = this.toLocationId });

            this.StoresPack.MoveStock(
                this.ReqNumber,
                3,
                this.PartNumber,
                this.Quantity,
                this.fromLocationId,
                null,
                this.FromStockDate,
                this.toLocationId,
                null,
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
        public void ShouldCheckLocation()
        {
            this.StorageLocationRepository.Received(2).FindBy(Arg.Any<Expression<Func<StorageLocation, bool>>>());
        }

        [Test]
        public void ShouldCheckFromLocationStock()
        {
            this.StoresPack.Received().CheckStockAtFromLocation(
                this.PartNumber,
                this.Quantity,
                this.From,
                this.fromLocationId,
                null,
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
                this.fromLocationId,
                null,
                this.FromStockDate,
                this.toLocationId,
                null,
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
