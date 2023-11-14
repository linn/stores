namespace Linn.Stores.Domain.LinnApps.Tests.WarehouseLocationTests
{
    using FluentAssertions;
    using NUnit.Framework;

    public class WhenCheckingScsLocationOfRackingLocation : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Sut.PalletId = 1000;
            this.Sut.Location = "SA01306R";
            this.Sut.XCoord = 13;
            this.Sut.YCoord = 6;
        }

        [Test]
        public void ShouldHaveRightStoresAddress()
        {
            this.Sut.ScsAreaCode().Should().Be(2);
            this.Sut.ScsColumnIndex().Should().Be(13);
            this.Sut.ScsLevelIndex().Should().Be(6);
        }
    }
}
