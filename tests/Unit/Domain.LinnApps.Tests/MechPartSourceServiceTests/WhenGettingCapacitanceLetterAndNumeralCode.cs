namespace Linn.Stores.Domain.LinnApps.Tests.MechPartSourceServiceTests
{
    using FluentAssertions;

    using NUnit.Framework;

    public class WhenGettingCapacitanceLetterAndNumeralCode : ContextBase
    {
        [Test]
        public void WhenUnitIsMicro()
        {
            this.Sut.GetCapacitanceLetterAndNumeralCode("u", 1m).Should().Be("1uF");
            this.Sut.GetCapacitanceLetterAndNumeralCode("u", 4.7m).Should().Be("4u7F");
            this.Sut.GetCapacitanceLetterAndNumeralCode("u", 470m).Should().Be("470uF");
        }

        [Test]
        public void WhenUnitIsNano()
        {
            this.Sut.GetCapacitanceLetterAndNumeralCode("n", 150000m).Should().Be("150000nF");
            this.Sut.GetCapacitanceLetterAndNumeralCode("n", 2.2m).Should().Be("2n2F");
            this.Sut.GetCapacitanceLetterAndNumeralCode("n", 1m).Should().Be("1nF");
            this.Sut.GetCapacitanceLetterAndNumeralCode("n", 100000m).Should().Be("100000nF");
        }

        [Test]
        public void WhenUnitIsPico()
        {
            this.Sut.GetRkmCode("p", 220m).Should().Be("220pF");
            this.Sut.GetRkmCode("p", 2.2m).Should().Be("2p2F");
            this.Sut.GetRkmCode("p", 220000000m).Should().Be("220000000pF");
        }
    }
}
