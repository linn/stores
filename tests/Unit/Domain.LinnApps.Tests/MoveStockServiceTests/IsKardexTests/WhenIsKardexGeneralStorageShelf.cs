namespace Linn.Stores.Domain.LinnApps.Tests.MoveStockServiceTests.IsKardexTests
{
    using FluentAssertions;

    using NUnit.Framework;

    public class WhenIsKardexGeneralStorageShelf : ContextBase
    {
        private bool result;

        [SetUp]
        public void SetUp()
        {
            this.result = this.Sut.IsKardexLocation("E-K4-01");
        }

        [Test]
        public void ShouldReturnTrue()
        {
            this.result.Should().BeFalse();
        }
    }
}
