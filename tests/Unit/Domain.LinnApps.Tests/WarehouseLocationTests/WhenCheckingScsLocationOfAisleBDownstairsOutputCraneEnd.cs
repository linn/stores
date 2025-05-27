namespace Linn.Stores.Domain.LinnApps.Tests.WarehouseLocationTests
{
    using FluentAssertions;
    using NUnit.Framework;

    public class WhenCheckingScsLocationOfAisleBDownstairsOutputCraneEnd : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Sut.PalletId = 1000;
            this.Sut.Location = "SBB";
        }

        [Test]
        public void ShouldHaveRightStoresAddress()
        {
            this.Sut.ScsAreaCode().Should().Be(3);
            this.Sut.ScsColumnIndex().Should().Be(1);
            this.Sut.ScsLevelIndex().Should().Be(1);
            this.Sut.ScsSideIndex().Should().Be(1);
        }
    }
}
