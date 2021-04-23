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

    public class WhenMovingStockFromKardex : ContextBase
    {
        private RequisitionProcessResult result;

        private int fromLocationId;

        private int toLocationId;

        private string storageType;

        [SetUp]
        public void SetUp()
        {
            this.From = "K1";
            this.To = "E-LOC-2";
            this.toLocationId = 34;
            this.storageType = "storage type 1";

            this.StoresPack.CheckStockAtFromLocation(
                    this.PartNumber,
                    this.Quantity,
                    this.From,
                    null,
                    null,
                    this.FromStockDate)
                .Returns(new RequisitionProcessResult(true, "ok"));

            this.StorageLocationRepository.FindBy(Arg.Any<Expression<Func<StorageLocation, bool>>>())
                .Returns(new StorageLocation { LocationId = this.toLocationId });

            this.KardexPack.MoveStockFromKardex(
                this.ReqNumber,
                3,
                this.From,
                this.PartNumber,
                this.Quantity,
                this.storageType,
                this.toLocationId,
                null)
                .Returns(new ProcessResult(true, "ok"));

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
                this.storageType,
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
            this.StorageLocationRepository.Received().FindBy(Arg.Any<Expression<Func<StorageLocation, bool>>>());
        }

        [Test]
        public void ShouldCheckFromLocationStock()
        {
            this.StoresPack.Received().CheckStockAtFromLocation(
                this.PartNumber,
                this.Quantity,
                this.From,
                null,
                null,
                this.FromStockDate);
        }

        [Test]
        public void ShouldDoMove()
        {
            this.KardexPack.Received().MoveStockFromKardex(
                this.ReqNumber,
                3,
                this.From,
                this.PartNumber,
                this.Quantity,
                this.storageType,
                this.toLocationId,
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
