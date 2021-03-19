namespace Linn.Stores.Facade.Tests.MoveStockFacadeServiceTests
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Resources.RequestResources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenMovingStock : ContextBase
    {
        private MoveStockRequestResource resource;

        private ProcessResult moveResult;

        private IResult<ProcessResult> result;

        [SetUp]
        public void SetUp()
        {
            this.moveResult = new ProcessResult { Message = "ok", Success = true };
            this.resource = new MoveStockRequestResource
                                {
                                    PartNumber = "P1",
                                    Quantity = 2,
                                    From = "L1",
                                    To = "L2"
                                };
            this.MoveStockService.MoveStock(
                    this.resource.ReqNumber,
                    this.resource.PartNumber,
                    this.resource.Quantity,
                    this.resource.From,
                    this.resource.FromLocationId,
                    this.resource.FromPalletNumber,
                    this.resource.To,
                    this.resource.ToLocationId,
                    this.resource.ToPalletNumber)
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
                this.resource.To,
                this.resource.ToLocationId,
                this.resource.ToPalletNumber);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<ProcessResult>>();
            var dataResult = ((SuccessResult<ProcessResult>)this.result).Data;
            dataResult.Message.Should().Be(this.moveResult.Message);
            dataResult.Success.Should().Be(this.moveResult.Success);
        }
    }
}
