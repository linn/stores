namespace Linn.Stores.Domain.LinnApps.Tests.GoodsInServiceTests
{
    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.GoodsIn;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenValidatingRsnAndErrorMessage : ContextBase
    {
        private ValidateRsnResult result;

        [SetUp]
        public void SetUp()
        {
            this.GoodsInPack.When(x => x.GetRsnDetails(1, out _, out _, out _, out _, out _, out _))
                .Do(x =>
                {
                    x[6] = "Can't book in";
                });

            this.GoodsInPack.GetRsnDetails(1, out _, out _, out _, out _, out _, out _)
                .Returns(false);

            this.result = this.Sut.ValidateRsn(1);
        }

        [Test]
        public void ShouldReturnErrorResult()
        {
            this.result.Success.Should().BeFalse();
            this.result.Message.Should().Be("Can't book in");
        }
    }
}
