namespace Linn.Stores.Domain.LinnApps.Tests.GoodsInServiceTests
{
    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using NSubstitute;

    using NUnit.Framework;

    public class WhenBookingInAndNumberOfBookInLinesLessThanNumberOfLinesParameter : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.PalletAnalysisPack.CanPutPartOnPallet("PART", "1234").Returns(true);
            this.Sut.DoBookIn(
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
        public void ShouldCreateGoodsInLogEntry()
        {
            this.GoodsInLog.Received().Add(Arg.Any<GoodsInLogEntry>());
        }
    }
}
