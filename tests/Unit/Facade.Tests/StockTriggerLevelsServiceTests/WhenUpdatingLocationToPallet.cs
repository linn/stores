namespace Linn.Stores.Facade.Tests.StockTriggerLevelsServiceTests
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.Parts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdatingLocationToPallet : ContextBase
    {
        private int stockTriggerLevelId = 1;

        private StockTriggerLevelsResource resource;

        private IResult<StockTriggerLevel> result;

        [SetUp]
        public void SetUp()
        {
            var stockTriggerLevel = new StockTriggerLevel
            {
                PartNumber = "CTC PART",
                Id = 1,
                KanbanSize = 1,
                LocationId = 123,
                MaxCapacity = 1,
                PalletNumber = 1,
                TriggerLevel = 1
            };

            this.resource = new StockTriggerLevelsResource
            {
                PartNumber = "ES PART",
                Id = 1,
                KanbanSize = 2,
                LocationId = 123,
                MaxCapacity = 12,
                PalletNumber = 456,
                TriggerLevel = 400
            };

            this.result = this.Sut.Add(this.resource);

            this.Repository.FindById(this.stockTriggerLevelId).Returns(stockTriggerLevel);

            this.result = this.Sut.Update(this.resource.Id, this.resource);
        }

        [Test]
        public void ShouldGet()
        {
            this.Repository.Received().FindById(this.stockTriggerLevelId);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<StockTriggerLevel>>();
            var dataResult = ((SuccessResult<StockTriggerLevel>)this.result).Data;
            dataResult.Id.Should().Be(this.stockTriggerLevelId);
            dataResult.PartNumber.Should().Be("ES PART");
            dataResult.KanbanSize.Should().Be(2);
            dataResult.LocationId.Should().Be(null);
            dataResult.MaxCapacity.Should().Be(12);
            dataResult.PalletNumber.Should().Be(456);
            dataResult.TriggerLevel.Should().Be(400);
        }
    }
}
