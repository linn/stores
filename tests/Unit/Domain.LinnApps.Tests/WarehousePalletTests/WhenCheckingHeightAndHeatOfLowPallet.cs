namespace Linn.Stores.Domain.LinnApps.Tests.WarehousePalletTests
{
    using FluentAssertions;
    using NUnit.Framework;

    public class WhenCheckingHeightAndHeatOfLowPallet : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Sut.PalletId = 1000;
            this.Sut.SizeCode = "L";
            this.Sut.SpeedFactor = 4;
        }

        [Test]
        public void ShouldHaveRightHeightAndHeat()
        {
            this.Sut.ScsHeight().Should().Be(0);
            this.Sut.ScsHeat().Should().Be(3);
        }
    }
}
