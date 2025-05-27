namespace Linn.Stores.Domain.Tests.ScsPalletTests
{
    using FluentAssertions;

    using NUnit.Framework;

    public class WhenGettingUserFriendlyNameOfAislePallet : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Sut.PalletNumber = 1000;
            this.Sut.Area = 3;
            this.Sut.Column = 29;
            this.Sut.Level = 6;
            this.Sut.Side = 0;
        }

        [Test]
        public void ShouldHaveRightAreaCode()
        {
            this.Sut.UserFriendlyName().Should().Be("SB02906L");
        }
    }
}
