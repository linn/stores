namespace Linn.Stores.Domain.LinnApps.Tests.GoodsInServiceTests
{
    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.GoodsIn;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenValidatingRsnAndNoErrorMessage : ContextBase
    {
        private ValidateRsnResult result;

        [SetUp]
        public void SetUp()
        {
            this.GoodsInPack.When(x => x.GetRsnDetails(
                1,
                out _,
                out _,
                out _,
                out _,
                out _,
                out _)).Do(x =>
                {
                    x[1] = "STATE";
                    x[2] = "ARTICLE";
                    x[3] = "DESC";
                    x[4] = 1;
                    x[5] = 123456;
                    x[6] = null;
                });
            this.GoodsInPack.GetRsnDetails(1, out _, out _, out _, out _, out _, out _)
                .Returns(true);
            this.result = this.Sut.ValidateRsn(1);
        }

        [Test]
        public void ShouldReturnSuccessResult()
        {
            this.result.Success.Should().BeTrue();
            this.result.ArticleNumber.Should().Be("ARTICLE");
            this.result.State.Should().Be("STATE");
            this.result.Description.Should().Be("DESC");
            this.result.Quantity.Should().Be(1);
            this.result.SerialNumber.Should().Be(123456);
        }
    }
}
