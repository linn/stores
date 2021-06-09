namespace Linn.Stores.Domain.LinnApps.Tests.MoveStockServiceTests.IsKardexTests
{
    using FluentAssertions;

    using NUnit.Framework;

    public class WhenIsKardexLocation : ContextBase
    {
        private bool result;

        [SetUp]
        public void SetUp()
        {
            this.result = this.Sut.IsKardexLocation("E-K1-") && this.Sut.IsKardexLocation("E-K2-");
        }

        [Test]
        public void ShouldReturnTrue()
        {
            this.result.Should().BeTrue();
        }
    }
}
