namespace Linn.Stores.Service.Tests.StockTriggerLevelsModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingById : ContextBase
    {
        private StockTriggerLevel stockTriggerLevel1;

        [SetUp]
        public void SetUp()
        {
            this.stockTriggerLevel1 = new StockTriggerLevel 
                                          { 
                                              PalletNumber = 123, 
                                              PartNumber = "SAMPLE PART", 
                                              TriggerLevel = 1, 
                                              LocationId = 35, 
                                              KanbanSize = 1, 
                                              MaxCapacity = 1
                                          };

            this.StockTriggerLevelsFacadeService.GetById(Arg.Any<int>())
                .Returns(new SuccessResult<StockTriggerLevel>(this.stockTriggerLevel1));

            this.Response = this.Browser.Get(
                "/inventory/stock-trigger-levels/35",
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
            this.StockTriggerLevelsFacadeService.Received().GetById(Arg.Any<int>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resultResource = this.Response.Body.DeserializeJson<StockTriggerLevel>();
            resultResource.LocationId.Should().Be(35);
        }
    }
}
