namespace Linn.Stores.Facade.Tests.MoveStockFacadeServiceTests
{
    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockMove.Models;
    using Linn.Stores.Resources.RequestResources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenMovingStock : ContextBase
    {
        private MoveStockRequestResource resource;

        private RequisitionProcessResult moveResult;

        private IResult<RequisitionProcessResult> result;

        [SetUp]
        public void SetUp()
        {
            this.moveResult = new RequisitionProcessResult { Message = "ok", Success = true };
            this.resource = new MoveStockRequestResource
                                {
                                    PartNumber = "P1",
                                    Quantity = 2,
                                    From = "L1",
                                    FromLocationId = 3,
                                    FromPalletNumber = 34,
                                    FromStockRotationDate = 21.March(2022).ToString("O"),
                                    FromState = "good",
                                    FromStockPoolCode = "pool 1",
                                    To = "L2",
                                    ToLocationId = 536,
                                    ToPalletNumber = 8000,
                                    ToStockRotationDate = 2.April(2024).ToString("O"),
                                    UserNumber = 909
                                };
            this.MoveStockService.MoveStock(
                    this.resource.ReqNumber,
                    this.resource.PartNumber,
                    this.resource.Quantity,
                    this.resource.From,
                    this.resource.FromLocationId,
                    this.resource.FromPalletNumber,
                    21.March(2022),
                    this.resource.FromState,
                    this.resource.FromStockPoolCode,
                    this.resource.To,
                    this.resource.ToLocationId,
                    this.resource.ToPalletNumber,
                    2.April(2024),
                    this.resource.UserNumber)
                .Returns(this.moveResult);

            this.result = this.Sut.MoveStock(this.resource);
        }

        [Test]
        public void ShouldCallService()
        {
            this.MoveStockService.Received().MoveStock(
                this.resource.ReqNumber,
                this.resource.PartNumber,
                this.resource.Quantity,
                this.resource.From,
                this.resource.FromLocationId,
                this.resource.FromPalletNumber,
                21.March(2022),
                this.resource.FromState,
                this.resource.FromStockPoolCode,
                this.resource.To,
                this.resource.ToLocationId,
                this.resource.ToPalletNumber,
                2.April(2024),
                this.resource.UserNumber);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<RequisitionProcessResult>>();
            var dataResult = ((SuccessResult<RequisitionProcessResult>)this.result).Data;
            dataResult.Message.Should().Be(this.moveResult.Message);
            dataResult.Success.Should().Be(this.moveResult.Success);
        }
    }
}
