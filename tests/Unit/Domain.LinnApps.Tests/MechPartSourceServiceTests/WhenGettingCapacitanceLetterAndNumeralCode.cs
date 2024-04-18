namespace Linn.Stores.Domain.LinnApps.Tests.MechPartSourceServiceTests
{
    using FluentAssertions;

    using NUnit.Framework;

    public class WhenGettingCapacitanceLetterAndNumeralCode : ContextBase
    {
        [Test]
        public void WhenUnitIsMicro()
        {
            this.Sut.GetCapacitanceLetterAndNumeralCode("u", 0.000001m).Should().Be("1U");
            this.Sut.GetCapacitanceLetterAndNumeralCode("u", 0.0000047m).Should().Be("4U7");
            this.Sut.GetCapacitanceLetterAndNumeralCode("u", 0.000470m).Should().Be("470U");
        }

        [Test]
        public void WhenUnitIsNano()
        {
            this.Sut.GetCapacitanceLetterAndNumeralCode("n", 0.000150000m).Should().Be("150000N");
            this.Sut.GetCapacitanceLetterAndNumeralCode("n", 0.0000000022m).Should().Be("2N2");
            this.Sut.GetCapacitanceLetterAndNumeralCode("n", 0.000000001m).Should().Be("1N");
            this.Sut.GetCapacitanceLetterAndNumeralCode("n", 0.000100000m).Should().Be("100000N");
        }

        [Test]
        public void WhenUnitIsPico()
        {
            this.Sut.GetCapacitanceLetterAndNumeralCode("p", 0.000000000220m).Should().Be("220P");
            this.Sut.GetCapacitanceLetterAndNumeralCode("p", 0.0000000000680m).Should().Be("68P");
            this.Sut.GetCapacitanceLetterAndNumeralCode("p", 0.0000000000022m).Should().Be("2P2");
            this.Sut.GetCapacitanceLetterAndNumeralCode("p", 0.000220000000m).Should().Be("220000000P");
        }
    }
}
