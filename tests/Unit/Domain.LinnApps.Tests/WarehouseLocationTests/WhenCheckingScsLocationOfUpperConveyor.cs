namespace Linn.Stores.Domain.LinnApps.Tests.WarehouseLocationTests
{
    using FluentAssertions;
    using NUnit.Framework;

    public class WhenCheckingScsLocationOfUpperConveyor : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Sut.PalletId = 1000;
            this.Sut.Location = "U6";
        }

        [Test]
        public void ShouldHaveRightStoresAddress()
        {
            this.Sut.ScsAreaCode().Should().Be(4);
            this.Sut.ScsColumnIndex().Should().Be(0);
            this.Sut.ScsLevelIndex().Should().Be(5);
            this.Sut.ScsSideIndex().Should().Be(1);
        }
    }
}
