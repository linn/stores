namespace Linn.Stores.Domain.LinnApps.Tests.WarehouseServiceTests
{
    using FluentAssertions;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenMovingPalletToUpper : ContextBase
    {
        private string result;

        private string reference;

        private int palletNumber;

        [SetUp]
        public void SetUp()
        {
            this.palletNumber = 123;
            this.reference = "987";
            this.WcsPack.CanMovePalletToUpper(this.palletNumber).Returns(true);
            this.result = this.Sut.MovePalletToUpper(this.palletNumber, this.reference);
        }

        [Test]
        public void ShouldCheckIfMoveStillPossible()
        {
            this.WcsPack.Received().CanMovePalletToUpper(this.palletNumber);
        }

        [Test]
        public void ShouldCallMove()
        {
            this.WcsPack.Received().MovePalletToUpper(this.palletNumber, this.reference);
        }

        [Test]
        public void ShouldReturnOkMessage()
        {
            this.result.Should().Be("Pallet 123 move to upper called successfully");
        }
    }
}
