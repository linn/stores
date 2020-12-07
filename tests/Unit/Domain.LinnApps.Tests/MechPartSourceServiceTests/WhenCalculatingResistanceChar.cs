namespace Linn.Stores.Domain.LinnApps.Tests.MechPartSourceServiceTests
{
    using FluentAssertions;

    using NUnit.Framework;

    public class WhenCalculatingResistanceChar : ContextBase
    {
        [Test]
        public void WhenUnitIsOhms()
        {
            this.Sut.CalculateResistanceChar(string.Empty, 1000).Should().Be("1000");
            this.Sut.CalculateResistanceChar(null, 0.47m).Should().Be("0.47");
        }

        [Test]
        public void WhenUnitIsKiloOhms()
        {
            this.Sut.CalculateResistanceChar("K", 0.8M).Should().Be("0.8K");
            this.Sut.CalculateResistanceChar("K", 1000).Should().Be("1K");
            this.Sut.CalculateResistanceChar("K", 1200).Should().Be("1K2");
            this.Sut.CalculateResistanceChar("K", 10000).Should().Be("10K");
        }

        [Test]
        public void WhenUnitIsMegaOhms()
        {
            this.Sut.CalculateResistanceChar("M", 0.8M).Should().Be("0.8M");
            this.Sut.CalculateResistanceChar("M", 1000000).Should().Be("1M");
            this.Sut.CalculateResistanceChar("M", 1200000).Should().Be("1M2");
            this.Sut.CalculateResistanceChar("M", 10000000).Should().Be("10M");
        }
    }
}
