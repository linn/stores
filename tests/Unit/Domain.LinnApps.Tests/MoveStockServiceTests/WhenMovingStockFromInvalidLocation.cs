namespace Linn.Stores.Domain.LinnApps.Tests.MoveStockServiceTests
{
    using System;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.StockMove.Models;

    using NUnit.Framework;

    public class WhenMovingStockFromInvalidLocation: ContextBase
    {
        private RequisitionProcessResult storesPackResult;

        private Action action;

        [SetUp]
        public void SetUp()
        {
            this.storesPackResult = new RequisitionProcessResult { Success = false };
            this.From = "XYZ-NO-LOC";
            this.To = "P2000";

            this.action = () => this.Sut.MoveStock(
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
        public void ShouldThrowException()
        {
            this.action.Should().Throw<TranslateLocationException>();
        }
    }
}
