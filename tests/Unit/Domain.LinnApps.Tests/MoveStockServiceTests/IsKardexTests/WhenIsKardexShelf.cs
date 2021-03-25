namespace Linn.Stores.Domain.LinnApps.Tests.MoveStockServiceTests.IsKardexTests
{
    using FluentAssertions;

    using NUnit.Framework;

    public class WhenIsKardexShelf : ContextBase
    {
        private bool result;

        [SetUp]
        public void SetUp()
        {
            this.result = this.Sut.IsKardexLocation("K1-") && this.Sut.IsKardexLocation("K2-")
                                                          && this.Sut.IsKardexLocation("K3-")
                                                          && this.Sut.IsKardexLocation("K4-")
                                                          && this.Sut.IsKardexLocation("K5-");
        }

        [Test]
        public void ShouldReturnTrue()
        {
            this.result.Should().BeTrue();
        }
    }
}
