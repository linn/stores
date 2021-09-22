namespace Linn.Stores.Domain.LinnApps.Tests.GoodsInServiceTests
{
    using FluentAssertions;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenValidatingPurchaseOrderAndStoredProcedureReturnsErrorMessage
    : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.GoodsInPack.When(x => x.GetPurchaseOrderDetails(
                    Arg.Any<int>(),
                    Arg.Any<int>(),
                    out Arg.Any<string>(),
                    out Arg.Any<string>(),
                    out Arg.Any<string>(),
                    out Arg.Any<int>(),
                    out Arg.Any<string>(),
                    out Arg.Any<string>(),
                    out Arg.Any<string>(),
                    out Arg.Any<string>()))
                .Do(x => x[9] = "Order is Overbooked");

        }

        [Test]
        public void ShouldReturnMessage()
        {
            this.Sut.ValidatePurchaseOrder(100, 1).Message.Should().Be("Order is Overbooked");
        }
    }
}
