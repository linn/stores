namespace Linn.Stores.Domain.LinnApps.Tests.WarehouseLocationTests
{
    using NUnit.Framework;

    using FluentAssertions;

    public class WhenCheckingScsLocationOfBench : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Sut.PalletId = 1000;
            this.Sut.Location = "B42";
        }

        [Test]
        public void ShouldHaveRightAreaCode()
        {
            this.Sut.ScsAreaCode().Should().Be(6);
            this.Sut.ScsColumnIndex().Should().Be(42);
            this.Sut.ScsLevelIndex().Should().Be(0);
            this.Sut.ScsSideIndex().Should().Be(0);
        }
    }
}
