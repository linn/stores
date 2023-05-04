namespace Linn.Stores.Service.Tests.StockTriggerLevelsModuleSpecs
{
    using System.Collections.Generic;

    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenAddingAndNotAuthorised : ContextBase
    {
        private StockTriggerLevel requestResource;

        [SetUp]
        public void SetUp()
        {
            this.requestResource = new StockTriggerLevel
                                        {
                                            PartNumber = "PART",
                                            Id = 1,
                                            KanbanSize = 1,
                                            LocationId = 1,
                                            MaxCapacity = 1,
                                            PalletNumber = 1,
                                            TriggerLevel = 1
                                        };
            var stockTriggerLevel = new StockTriggerLevel
                                        { 
                                            PartNumber = "PART",
                                            Id = 1,
                                            KanbanSize = 1,
                                            LocationId = 1,
                                            MaxCapacity = 1,
                                            PalletNumber = 1,
                                            TriggerLevel = 1
                                        };

            this.StockTriggerLevelsFacadeService.Add(Arg.Any<StockTriggerLevelsResource>())
                .Returns(new SuccessResult<StockTriggerLevel>(stockTriggerLevel));

            this.Response = this.Browser.Post(
                "/inventory/stock-trigger-levels/create",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Header("Content-Type", "application/json");
                        with.JsonBody(this.requestResource);
                    }).Result;
        }

        [Test]
        public void ShouldReturnBadRequest()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
