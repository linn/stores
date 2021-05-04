namespace Linn.Stores.Facade.Tests.MoveStockFacadeServiceTests
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockMove.Models;
    using Linn.Stores.Resources.RequestResources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenMovingStockFails : ContextBase
    {
        private MoveStockRequestResource resource;

        private RequisitionProcessResult moveResult;

        private IResult<RequisitionProcessResult> result;

        [SetUp]
        public void SetUp()
        {
            this.moveResult = new RequisitionProcessResult { Message = "not ok", Success = false };
            this.resource = new MoveStockRequestResource
                                {
                                    PartNumber = "P1",
                                    Quantity = 2,
                                    From = "L1",
                                    To = "L2",
                                    UserNumber = 32432
                                };
            this.MoveStockService.MoveStock(
                    this.resource.ReqNumber,
                    this.resource.PartNumber,
                    this.resource.Quantity,
                    this.resource.From,
                    this.resource.FromLocationId,
                    this.resource.FromPalletNumber,
                    null,
                    null,
                    null,
                    this.resource.To,
                    this.resource.ToLocationId,
                    this.resource.ToPalletNumber,
                    null,
                    null,
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
                    null,
                    null,
                null,
                this.resource.To,
                this.resource.ToLocationId,
                this.resource.ToPalletNumber,
                null,
                null,
                this.resource.UserNumber);
        }

        [Test]
        public void ShouldReturnBadRequest()
        {
            this.result.Should().BeOfType<BadRequestResult<RequisitionProcessResult>>();
            var dataResult = (BadRequestResult<RequisitionProcessResult>)this.result;
            dataResult.Message.Should().Be(this.moveResult.Message);
        }
    }
}
