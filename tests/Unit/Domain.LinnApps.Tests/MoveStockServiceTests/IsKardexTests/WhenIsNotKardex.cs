namespace Linn.Stores.Domain.LinnApps.Tests.MoveStockServiceTests.IsKardexTests
{
    using FluentAssertions;

    using NUnit.Framework;

    public class WhenIsNotKardex : ContextBase
    {
        private bool result;

        [SetUp]
        public void SetUp()
        {
            this.result = this.Sut.IsKardexLocation("P1003");
        }

        [Test]
        public void ShouldReturnFalse()
        {
            this.result.Should().BeFalse();
        }
    }
}
