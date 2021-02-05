namespace Linn.Stores.Facade.Tests.WarehouseFacadeServiceTests
{
    using FluentAssertions;

    using Linn.Common.Facade;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenMovingAllPalletsToUpper : ContextBase
    {
        private IResult<string> result;

        [SetUp]
        public void SetUp()
        {
            this.WarehouseService.MoveAllPalletsToUpper()
                .Returns("ok");

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
            this.result.Should().BeOfType<SuccessResult<string>>();
            var dataResult = ((SuccessResult<string>)this.result).Data;
            dataResult.Should().Be("ok");
        }
    }
}
