namespace Linn.Stores.Domain.LinnApps.Tests.GoodsInServiceTests
{
    using System;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Parts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenValidatingPurchaseOrderAndPartNotQcOnReceipt : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.PartsRepository.FindBy(Arg.Any<Expression<Func<Part, bool>>>()).Returns(new Part
                {
                    QcInformation = "I have some information",
                    QcOnReceipt = "N"
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

        }

        [Test]
        public void ShouldSetStateStores()
        {
            this.Sut.ValidatePurchaseOrder(100, 1).State.Should().Be("STORES");
        }
    }
}
