namespace Linn.Stores.Domain.Tests.ScsPalletTests
{
    using FluentAssertions;

    using NUnit.Framework;

    public class WhenGettingUserFriendlyNameOfUpperConveyor : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Sut.PalletNumber = 1000;
            this.Sut.Area = 2;
            this.Sut.Column = 0;
            this.Sut.Level = 5;
            this.Sut.Side = 1;
        }

        [Test]
        public void ShouldHaveRightAreaCode()
        {
            this.Sut.UserFriendlyName().Should().Be("U2");
        }
    }
}
