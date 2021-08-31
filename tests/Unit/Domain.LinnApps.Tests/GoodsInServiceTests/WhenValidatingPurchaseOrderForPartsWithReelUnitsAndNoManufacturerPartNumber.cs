namespace Linn.Stores.Domain.LinnApps.Tests.GoodsInServiceTests
{
    using System;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Parts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenValidatingPurchaseOrderForPartsWithReelUnitsAndNoManufacturerPartNumber 
        : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.PartsRepository.FindBy(Arg.Any<Expression<Func<Part, bool>>>()).Returns(new Part
                {
                    OurUnitOfMeasure = "REEL"
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
                .Do(x =>
                    {
                        x[4] = "REEL";
                        x[7] = null;
                    });
        }

        [Test]
        public void ShouldSetStateStores()
        {
            this.Sut.ValidatePurchaseOrder(100, 1).Message
                .Contains(
                    "No manufacturer part number on part supplier - see Purchasing")
                .Should().BeTrue();
        }
    }
}
