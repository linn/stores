namespace Linn.Stores.Domain.LinnApps.Tests.WarehouseLocationTests
{
    using FluentAssertions;
    using NUnit.Framework;

    public class WhenCheckingScsLocationOfCraneC : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Sut.PalletId = 1000;
            this.Sut.Location = "CC";
        }

        [Test]
        public void ShouldHaveRightAreaCode()
        {
            this.Sut.ScsAreaCode().Should().Be(4);
        }
    }
}
