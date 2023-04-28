namespace Linn.Stores.Service.Tests.StockTriggerLevelsModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Resources.StockLocators;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingAll : ContextBase
    {
        private StockTriggerLevel stockTriggerLevel1;

        private StockTriggerLevel stockTriggerLevel2;

        [SetUp]
        public void SetUp()
        {
            this.stockTriggerLevel1 = new StockTriggerLevel { PalletNumber = 123, PartNumber = "SAMPLE PART", TriggerLevel = 1, LocationId = 35, KanbanSize = 1, MaxCapacity = 1 };
            this.stockTriggerLevel2 = new StockTriggerLevel { PalletNumber = 456, PartNumber = "TEST PART", TriggerLevel = 2, LocationId = 33, KanbanSize = 1, MaxCapacity = 2 };

            this.StockTriggerLevelsFaceFacadeService.GetAll()
                .Returns(new SuccessResult<IEnumerable<StockTriggerLevel>>(new List<StockTriggerLevel>
                                                                              {
                                                                                  this.stockTriggerLevel1, this.stockTriggerLevel2
                                                                              }));

            this.Response = this.Browser.Get(
                "/inventory/stock-trigger-levels",
                with =>
                {
                    with.Header("Accept", "application/json");
                }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.StockTriggerLevelsFaceFacadeService.Received().GetAll();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resultResource = this.Response.Body.DeserializeJson<IEnumerable<StockPoolResource>>().ToList();
            resultResource.Should().HaveCount(2);
            resultResource.First(a => a.Id == 1).StockPoolCode.Should().Be("one");
            resultResource.First(a => a.Id == 2).StockPoolCode.Should().Be("two");
        }
    }
}
