namespace Linn.Stores.Domain.LinnApps.Tests.WarehouseServiceTests
{
    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenMovingPalletToUpperButNoLongerPossible : ContextBase
    {
        private string reference;

        private int palletNumber;

        private MessageResult result;

        [SetUp]
        public void SetUp()
        {
            this.palletNumber = 123;
            this.reference = "987";
            this.WcsPack.CanMovePalletToUpper(this.palletNumber).Returns(false);
            this.result = this.Sut.MovePalletToUpper(this.palletNumber, this.reference);
        }

        [Test]
        public void ShouldCheckIfMoveStillPossible()
        {
            this.WcsPack.Received().CanMovePalletToUpper(this.palletNumber);
        }

        [Test]
        public void ShouldNotCallMove()
        {
            this.WcsPack.DidNotReceive().MovePalletToUpper(Arg.Any<int>(), Arg.Any<string>());
        }

        [Test]
        public void ShouldReturnMessage()
        {
            this.result.Message.Should().Be("Pallet 123 can no longer be moved to upper");
        }
    }
}
