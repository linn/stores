namespace Linn.Stores.Domain.LinnApps.Tests.GoodsInServiceTests
{
    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenValidatingPurchaseOrderQty : ContextBase
    {
        private ProcessResult result;

        [SetUp]
        public void SetUp()
        {
            this.StoresPack.ValidOrderQty(1, 1, 1, out var _, out var _).Returns(true);
            this.result = this.Sut.ValidatePurchaseOrderQty(1, 1, 1);
        }

        [Test]
        public void ShouldReturnSuccessResult()
        {
            this.result.Success.Should().BeTrue();
        }
    }
}
