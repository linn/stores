namespace Linn.Stores.Facade.Tests.WarehouseFacadeServiceTests
{
    using FluentAssertions;

    using Linn.Common.Facade;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenMovingPalletToUpper : ContextBase
    {
        private IResult<string> result;

        private string reference;

        private int palletNumber;

        [SetUp]
        public void SetUp()
        {
            this.palletNumber = 808;
            this.reference = "567";

            this.WarehouseService.MovePalletToUpper(this.palletNumber, this.reference)
                .Returns("ok");

            this.result = this.Sut.MovePalletToUpper(this.palletNumber, this.reference);
        }

        [Test]
        public void ShouldCallService()
        {
            this.WarehouseService.Received().MovePalletToUpper(this.palletNumber, this.reference);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<string>>();
            var dataResult = ((SuccessResult<string>)this.result).Data;
            dataResult.Should().Be("ok");
        }
    }
}
