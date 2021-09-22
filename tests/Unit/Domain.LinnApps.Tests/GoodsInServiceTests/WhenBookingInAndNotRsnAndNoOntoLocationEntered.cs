namespace Linn.Stores.Domain.LinnApps.Tests.GoodsInServiceTests
{
    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Domain.LinnApps.Models;

    using NUnit.Framework;

    public class WhenBookingInAndNotRsnAndNoOntoLocationEntered : ContextBase
    {
        private ProcessResult processResult;

        [SetUp]
        public void SetUp()
        {
            this.processResult = this.Sut.DoBookIn(
                "O",
                1,
                "PART",
                null,
                1,
                1,
                1,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                1,
                false,
                new GoodsInLogEntry[0]);
        }

        [Test]
        public void ShouldFailWithMessage()
        {
            this.processResult.Success.Should().BeFalse();
            this.processResult.Message.Should().Be("Onto location/pallet must be entered");
        }
    }
}
