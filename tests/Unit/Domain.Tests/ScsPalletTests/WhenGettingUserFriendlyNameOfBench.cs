namespace Linn.Stores.Domain.Tests.ScsPalletTests
{
    using FluentAssertions;
    using NUnit.Framework;

    public class WhenGettingUserFriendlyNameOfBench : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Sut.PalletNumber = 1000;
            this.Sut.Area = 6;
            this.Sut.Column = 29;
            this.Sut.Level = 0;
            this.Sut.Side = 0;
        }

        [Test]
        public void ShouldHaveRightAreaCode()
        {
            this.Sut.UserFriendlyName().Should().Be("B29");
        }
    }
}
