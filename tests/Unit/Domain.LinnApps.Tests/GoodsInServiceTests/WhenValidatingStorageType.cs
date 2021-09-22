namespace Linn.Stores.Domain.LinnApps.Tests.GoodsInServiceTests
{
    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.GoodsIn;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenValidatingStorageType : ContextBase
    {
        private ValidateStorageTypeResult result;

        [SetUp]
        public void SetUp()
        {
            this.GoodsInPack.When(x => x.GetKardexLocations(
                1, 
                "PO",
                null,
                null,
                out _,
                out _,
                null))
                .Do(
                    x =>
                        {
                            x[4] = 1;
                            x[5] = "LOCATION 1";
                        });

            this.GoodsInPack.GetErrorMessage().Returns("SOME ERROR MESSAGE");

            this.result = this.Sut.ValidateStorageType(1, "PO", null, null, null);
        }

        [Test]
        public void ShouldReturnResult()
        {
            this.result.Message.Should().Be("SOME ERROR MESSAGE");
            this.result.LocationId.Should().Be(1);
            this.result.LocationCode.Should().Be("LOCATION 1");
        }
    }
}
