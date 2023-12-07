namespace Linn.Stores.Facade.Tests.StockTriggerLevelsServiceTests
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenAdding : ContextBase
    {
        private readonly int stockTriggerLevelId = 1;

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
        }

        [Test]
        public void ShouldAdd()
        {
            this.Repository.Received().Add(Arg.Any<StockTriggerLevel>());
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<CreatedResult<StockTriggerLevel>>();
            var dataResult = ((CreatedResult<StockTriggerLevel>)this.result).Data;
            dataResult.Id.Should().Be(0);
            dataResult.PartNumber.Should().Be("ES PART");
            dataResult.KanbanSize.Should().Be(2);
            dataResult.LocationId.Should().Be(null);
            dataResult.MaxCapacity.Should().Be(12);
            dataResult.PalletNumber.Should().Be(456);
            dataResult.TriggerLevel.Should().Be(400);
        }
    }
}
