namespace Linn.Stores.Domain.LinnApps.Tests.MoveStockServiceTests.IsKardexTests
{
    using FluentAssertions;

    using NUnit.Framework;

    public class WhenIsShortLocation : ContextBase
    {
        private bool result;

        [SetUp]
        public void SetUp()
        {
            this.result = this.Sut.IsKardexLocation("MT");
        }

        [Test]
        public void ShouldReturnFalse()
        {
            this.result.Should().BeFalse();
        }
    }
}
