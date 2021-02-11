namespace Linn.Stores.Facade.Tests.WarehouseFacadeServiceTests
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenMovingAllPalletsToUpper : ContextBase
    {
        private IResult<MessageResult> result;

        [SetUp]
        public void SetUp()
        {
            this.WarehouseService.MoveAllPalletsToUpper()
                .Returns(new MessageResult("ok"));

            this.result = this.Sut.MoveAllPalletsToUpper();
        }

        [Test]
        public void ShouldCallService()
        {
            this.WarehouseService.Received().MoveAllPalletsToUpper();
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
