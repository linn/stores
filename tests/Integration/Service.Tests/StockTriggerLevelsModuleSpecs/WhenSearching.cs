namespace Linn.Stores.Service.Tests.StockTriggerLevelsModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.StockLocators;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenSearchingLocationCodes : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var stockTriggerLevelA = new StockTriggerLevel
                                         {
                                             PartNumber = "PART",
                                             Id = 1,
                                             KanbanSize = 1,
                                             LocationId = 256,
                                             MaxCapacity = 1,
                                             TriggerLevel = 1,
                                             PalletNumber = 0,
                                             StorageLocation = new StorageLocation()
                                                                   {
                                                                       LocationCode = "123",
                                                                       Description = "A Location Description"
                                                                   }
            };
            var stockTriggerLevelB = new StockTriggerLevel
                                        {
                                            PartNumber = "PART 2",
                                            Id = 2,
                                            KanbanSize = 2,
                                            LocationId = 256,
                                            MaxCapacity = 2,
                                            TriggerLevel = 2,
                                            PalletNumber = 0,
                                            StorageLocation = new StorageLocation()
                                                                  {
                                                                      LocationCode = "123",
                                                                      Description = "A Location Description"
                                                                  }
            };

            this.StockTriggerLevelsFacadeService.SearchStockTriggerLevelsWithWildcard("P*", "123")
                .Returns(new SuccessResult<IEnumerable<StockTriggerLevel>>(new List<StockTriggerLevel> { stockTriggerLevelA, stockTriggerLevelB }));

            this.Response = this.Browser.Get(
                "/inventory/stock-trigger-levels",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("partNumberSearchTerm", "P*");
                        with.Query("storagePlaceSearchTerm", "123");
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
            this.StockTriggerLevelsFacadeService.SearchStockTriggerLevelsWithWildcard("P*", "123");
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<StockTriggerLevelsResource>>().ToList();
            resource.Should().HaveCount(2);
            resource.Should().Contain(a => a.PartNumber == "PART");
            resource.Should().Contain(a => a.PartNumber == "PART 2");
        }
    }
}