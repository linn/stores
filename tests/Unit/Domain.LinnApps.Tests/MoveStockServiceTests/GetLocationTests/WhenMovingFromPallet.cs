namespace Linn.Stores.Domain.LinnApps.Tests.MoveStockServiceTests.GetLocationTests
{
    using FluentAssertions;

    using NUnit.Framework;

    public class WhenMovingFromPallet : ContextBase
    {
        private int? palletNumber;

        private int? locationId;

        [SetUp]
        public void SetUp()
        {
            this.Sut.GetLocationDetails("P2000", out this.locationId, out this.palletNumber);
        }

        [Test]
        public void ShouldReturnPalletNumber()
        {
            this.locationId.Should().BeNull();
            this.palletNumber.Should().Be(2000);
        }
    }
}
