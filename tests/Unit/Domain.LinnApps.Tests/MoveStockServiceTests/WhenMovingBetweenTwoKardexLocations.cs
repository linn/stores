namespace Linn.Stores.Domain.LinnApps.Tests.MoveStockServiceTests
{
    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.StockMove.Models;

    using NUnit.Framework;

    public class WhenMovingBetweenTwoKardexLocations : ContextBase
    {
        private RequisitionProcessResult result;

        [SetUp]
        public void SetUp()
        {
            this.From = "E-K1-34-34";
            this.To = "K2";

            this.result = this.Sut.MoveStock(
                this.ReqNumber,
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
                null,
                this.UserNumber);
        }

        [Test]
        public void ShouldReturnErrorMesage()
        {
            this.result.Success.Should().BeFalse();
            this.result.Message.Should().Be("You cannot move between two Kardex locations.");
        }
    }
}
