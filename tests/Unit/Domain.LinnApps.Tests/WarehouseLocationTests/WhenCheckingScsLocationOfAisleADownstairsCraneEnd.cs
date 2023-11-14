namespace Linn.Stores.Domain.LinnApps.Tests.WarehouseLocationTests
{
    using FluentAssertions;
    using NUnit.Framework;

    public class WhenCheckingScsLocationOfAisleADownstairsCraneEnd : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Sut.PalletId = 1000;
            this.Sut.Location = "SAA";
        }

        [Test]
        public void ShouldHaveRightStoresAddress()
        {
            this.Sut.ScsAreaCode().Should().Be(2);
            this.Sut.ScsColumnIndex().Should().Be(1);
            this.Sut.ScsLevelIndex().Should().Be(1);
            this.Sut.ScsSideIndex().Should().Be(0);
        }
    }
}
