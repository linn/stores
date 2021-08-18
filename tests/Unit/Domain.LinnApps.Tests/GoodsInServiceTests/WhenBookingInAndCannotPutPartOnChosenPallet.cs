namespace Linn.Stores.Domain.LinnApps.Tests.GoodsInServiceTests
{
    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Domain.LinnApps.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenBookingInAndCannotPutPartOnChosenPallet : ContextBase
    {
        private ProcessResult processResult;

        [SetUp]
        public void SetUp()
        {
            this.PalletAnalysisPack.CanPutPartOnPallet("PART", "1234").Returns(false);
            this.PalletAnalysisPack.Message().Returns("Cannot put a PART part on a PALLET pallet");
            this.processResult = this.Sut.DoBookIn(
                "O",
                1,
                partNumber: "PART",
                null,
                1,
                1,
                1,
                null,
                null,
                null,
                null,
                null,
                ontoLocation: "P1234",
                null,
                null,
                null,
                null,
                null,
                1,
                new GoodsInLogEntry[0]);
        }

        [Test]
        public void ShouldFailWithMessage()
        {
            this.processResult.Success.Should().BeFalse();
            this.processResult.Message.Should().Be("Cannot put a PART part on a PALLET pallet");
        }
    }
}
