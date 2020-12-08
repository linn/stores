namespace Linn.Stores.Domain.LinnApps.Tests.MechPartSourceServiceTests
{
    using FluentAssertions;

    using NUnit.Framework;

    public class WhenGettingRkmCode : ContextBase
    {
        [Test]
        public void WhenUnitIsOhms()
        {
            this.Sut.GetRkmCode(string.Empty, 1000).Should().Be("1000");
            this.Sut.GetRkmCode(null, 0.47m).Should().Be("0.47");
        }

        [Test]
        public void WhenUnitIsKiloOhms()
        {
            this.Sut.GetRkmCode("K", 0.8M).Should().Be("0.8K");
            this.Sut.GetRkmCode("K", 1000).Should().Be("1K");
            this.Sut.GetRkmCode("K", 1200).Should().Be("1K2");
            this.Sut.GetRkmCode("K", 10000).Should().Be("10K");
        }

        [Test]
        public void WhenUnitIsMegaOhms()
        {
            this.Sut.GetRkmCode("M", 0.8M).Should().Be("0.8M");
            this.Sut.GetRkmCode("M", 1000000).Should().Be("1M");
            this.Sut.GetRkmCode("M", 1200000).Should().Be("1M2");
            this.Sut.GetRkmCode("M", 10000000).Should().Be("10M");
        }
    }
}
