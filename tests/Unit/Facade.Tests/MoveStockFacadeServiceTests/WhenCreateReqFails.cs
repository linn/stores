namespace Linn.Stores.Facade.Tests.MoveStockFacadeServiceTests
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.StockMove.Models;
    using Linn.Stores.Resources.RequestResources;

    using NSubstitute.ExceptionExtensions;

    using NUnit.Framework;

    public class WhenCreateReqFails : ContextBase
    {
        private MoveStockRequestResource resource;

        private IResult<RequisitionProcessResult> result;

        [SetUp]
        public void SetUp()
        {
            this.resource = new MoveStockRequestResource
                                {
                                    PartNumber = "P1",
                                    Quantity = 2,
                                    From = "L1",
                                    To = "L2",
                                    UserNumber = 234
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
                    this.resource.UserNumber)
                .Throws(new CreateReqFailureException("boom"));

            this.result = this.Sut.MoveStock(this.resource);
        }

        [Test]
        public void ShouldReturnBadRequest()
        {
            this.result.Should().BeOfType<BadRequestResult<RequisitionProcessResult>>();
            var dataResult = (BadRequestResult<RequisitionProcessResult>)this.result;
            dataResult.Message.Should().Be("boom");
        }
    }
}
