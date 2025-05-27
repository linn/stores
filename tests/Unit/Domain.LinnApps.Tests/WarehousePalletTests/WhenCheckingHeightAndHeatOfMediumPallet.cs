namespace Linn.Stores.Domain.LinnApps.Tests.WarehousePalletTests
{
    using FluentAssertions;

    using NUnit.Framework;

    public class WhenCheckingHeightAndHeatOfMediumPallet : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Sut.PalletId = 1000;
            this.Sut.SizeCode = "M";
            this.Sut.SpeedFactor = 3;
        }

        [Test]
        public void ShouldHaveRightHeightAndHeat()
        {
            this.Sut.ScsHeight().Should().Be(1);
            this.Sut.ScsHeat().Should().Be(2);
        }
    }
}
