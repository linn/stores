namespace Linn.Stores.Domain.LinnApps.Tests.GoodsInServiceTests
{
    using System;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Domain.LinnApps.Parts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenValidatingPurchaseOrderAndPartDoesNotHaveStorageTypeAndPartIsNew : ContextBase
    {
        private ValidatePurchaseOrderResult result;

        [SetUp]
        public void SetUp()
        {
            this.PartsRepository.FindBy(Arg.Any<Expression<Func<Part, bool>>>()).Returns(new Part
                {
                    QcInformation = "I have some information"
                });

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
                .Do(x => x[2] = "PART");

            this.GoodsInPack.PartHasStorageType("PART", out Arg.Any<int>(), out Arg.Any<string>(), out Arg.Any<bool>())
                .Returns(false);
            this.GoodsInPack.When(x => x.PartHasStorageType(
                    "PART",
                    out Arg.Any<int>(),
                    out Arg.Any<string>(),
                    out Arg.Any<bool>()))
                .Do(x => x[3] = true);

            this.result = this.Sut.ValidatePurchaseOrder(100, 1);
        }

        [Test]
        public void ShouldSetMessage()
        {
            this.result.BookInMessage.Should().Be("New part - enter storage type or location");
        }
    }
}
