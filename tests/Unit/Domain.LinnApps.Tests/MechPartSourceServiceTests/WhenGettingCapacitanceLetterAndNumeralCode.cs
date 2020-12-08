namespace Linn.Stores.Domain.LinnApps.Tests.MechPartSourceServiceTests
{
    using FluentAssertions;

    using NUnit.Framework;

    public class WhenGettingCapacitanceLetterAndNumeralCode : ContextBase
    {
        [Test]
        public void WhenUnitIsMicro()
        {
            this.Sut.GetCapacitanceLetterAndNumeralCode("u", 0.000001m).Should().Be("1uF");
            this.Sut.GetCapacitanceLetterAndNumeralCode("u", 0.0000047m).Should().Be("4u7F");
            this.Sut.GetCapacitanceLetterAndNumeralCode("u", 0.000470m).Should().Be("470uF");
        }

        [Test]
        public void WhenUnitIsNano()
        {
            this.Sut.GetCapacitanceLetterAndNumeralCode("n", 0.000150000m).Should().Be("150000nF");
            this.Sut.GetCapacitanceLetterAndNumeralCode("n", 0.0000000022m).Should().Be("2n2F");
            this.Sut.GetCapacitanceLetterAndNumeralCode("n", 0.000000001m).Should().Be("1nF");
            this.Sut.GetCapacitanceLetterAndNumeralCode("n", 0.000100000m).Should().Be("100000nF");
        }

        [Test]
        public void WhenUnitIsPico()
        {
            this.Sut.GetCapacitanceLetterAndNumeralCode("p", 0.000000000220m).Should().Be("220pF");
            this.Sut.GetCapacitanceLetterAndNumeralCode("p", 0.0000000000022m).Should().Be("2p2F");
            this.Sut.GetCapacitanceLetterAndNumeralCode("p", 0.000220000000m).Should().Be("220000000pF");
        }
    }
}
