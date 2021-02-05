namespace Linn.Stores.Domain.LinnApps.Tests.WarehouseServiceTests
{
    using FluentAssertions;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenMovingAllPalletsToUpper : ContextBase
    {
        private string result;

        [SetUp]
        public void SetUp()
        {
            this.result = this.Sut.MoveAllPalletsToUpper();
        }

        [Test]
        public void ShouldCallMoveAll()
        {
            this.WcsPack.Received().MoveDpqPalletsToUpper();
        }

        [Test]
        public void ShouldReturnOkMessage()
        {
            this.result.Should().Be("Move pallets to upper called successfully");
        }
    }
}
