namespace Linn.Stores.Domain.LinnApps.Tests.WarehousePalletTests
{
    using FluentAssertions;
    using NUnit.Framework;

    public class WhenCheckingHeightAndHeatOfHighPallet : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Sut.PalletId = 1000;
            this.Sut.SizeCode = "H";
            this.Sut.SpeedFactor = 2;
            this.Sut.RotationAverage = 3;
            this.Sut.Heat = 2;
        }

        [Test]
        public void ShouldHaveRightHeightAndHeat()
        {
            this.Sut.ScsHeight().Should().Be(2);
            this.Sut.ScsHeat().Should().Be(1);
        }
    }
}
