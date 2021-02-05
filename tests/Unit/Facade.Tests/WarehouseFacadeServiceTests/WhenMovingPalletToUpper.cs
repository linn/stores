namespace Linn.Stores.Facade.Tests.WarehouseFacadeServiceTests
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenMovingPalletToUpper : ContextBase
    {
        private IResult<MessageResult> result;

        private string reference;

        private int palletNumber;

        [SetUp]
        public void SetUp()
        {
            this.palletNumber = 808;
            this.reference = "567";

            this.WarehouseService.MovePalletToUpper(this.palletNumber, this.reference)
                .Returns(new MessageResult("ok"));

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
            this.result.Should().BeOfType<SuccessResult<MessageResult>>();
            var dataResult = ((SuccessResult<MessageResult>)this.result).Data;
            dataResult.Message.Should().Be("ok");
        }
    }
}
