namespace Linn.Stores.Facade.Tests.WandFacadeServiceTests
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Wand.Models;
    using Linn.Stores.Resources.Wand;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenWandingItem : ContextBase
    {
        private WandItemRequestResource resource;

        private WandResult wandServiceResult;

        private IResult<WandResult> result;

        [SetUp]
        public void SetUp()
        {
            this.resource = new WandItemRequestResource { ConsignmentId = 1, WandString = "wa", WandAction = "W" };
            this.wandServiceResult = new WandResult { Message = "ok", Success = true };
            this.WandService.Wand(this.resource.WandAction, this.resource.WandString, this.resource.ConsignmentId)
                .Returns(this.wandServiceResult);

            this.result = this.Sut.WandItem(this.resource);
        }

        [Test]
        public void ShouldCallService()
        {
            this.WandService.Received().Wand(
                this.resource.WandAction,
                this.resource.WandString,
                this.resource.ConsignmentId);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<WandResult>>();
            var dataResult = ((SuccessResult<WandResult>)this.result).Data;
            dataResult.Message.Should().Be(this.wandServiceResult.Message);
            dataResult.Success.Should().Be(this.wandServiceResult.Success);
        }
    }
}
