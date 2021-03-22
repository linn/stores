namespace Linn.Stores.Domain.LinnApps.Tests.MoveStockServiceTests
{
    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.StockMove.Models;

    using NUnit.Framework;

    public class WhenMovingWithInvalidQuantity : ContextBase
    {
        private RequisitionProcessResult result;

        [SetUp]
        public void SetUp()
        {
            this.ReqNumber = 1;
            this.From = "P2";
            this.To = "P1";
            this.Quantity = 0;

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
        public void ShouldReturnErrorMesage()
        {
            this.result.Success.Should().BeFalse();
            this.result.Message.Should().Be("You must have a valid quantity.");
        }
    }
}
