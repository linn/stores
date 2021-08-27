namespace Linn.Stores.Domain.LinnApps.Tests.GoodsInServiceTests
{
    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using NSubstitute;

    using NUnit.Framework;

    public class WhenBookingInAndNoBookInLines : ContextBase
    {
        private BookInResult result;

        [SetUp]
        public void SetUp()
        {
            this.PalletAnalysisPack.CanPutPartOnPallet("PART", "1234").Returns(true);
            this.result = this.Sut.DoBookIn(
                "O",
                1,
                "PART",
                null,
                1,
                1,
                1,
                null,
                null,
                null,
                null,
                null,
                "P1234",
                null,
                null,
                null,
                null,
                null,
                1,
                new GoodsInLogEntry[0]);
        }

        [Test]
        public void ShouldReturnFailState()
        {
            this.result.Success.Should().BeFalse();
            this.result.Message.Should().Be("Nothing to book in");
        }
    }
}
