namespace Linn.Stores.Domain.LinnApps.Tests.MoveStockServiceTests
{
    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.StockMove.Models;

    using NUnit.Framework;

    public class WhenMovingStockWithNoFromOrTo : ContextBase
    {
        private RequisitionProcessResult result;

        [SetUp]
        public void SetUp()
        {
            this.ReqNumber = 1;
            this.From = "P1000";

            this.result = this.Sut.MoveStock(
                this.ReqNumber,
                this.PartNumber,
                this.Quantity,
                this.From,
                null,
                null,
                null,
                string.Empty, 
                null,
                null,
                null,
                this.UserNumber);
        }

        [Test]
        public void ShouldReturnErrorMesage()
        {
            this.result.Success.Should().BeFalse();
            this.result.Message.Should().Be("A from and to location must be specified.");
        }
    }
}
