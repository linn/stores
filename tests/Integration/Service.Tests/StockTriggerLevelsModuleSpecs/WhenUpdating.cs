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

    public class WhenUpdating : ContextBase
    {
        private StockTriggerLevelsResource requestResource;

        [SetUp]
        public void SetUp()
        {
            this.requestResource = new StockTriggerLevelsResource
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

            this.AuthorisationService.HasPermissionFor("stock-trigger-level.update", Arg.Any<IEnumerable<string>>()).Returns(true);

            this.StockTriggerLevelsFacadeService.Update(1, Arg.Any<StockTriggerLevelsResource>())
                .Returns(new SuccessResult<StockTriggerLevel>(stockTriggerLevel));

            this.Response = this.Browser.Put(
                "/inventory/stock-trigger-levels/1",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Header("Content-Type", "application/json");
                        with.JsonBody(this.requestResource);
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
            this.StockTriggerLevelsFacadeService
                .Received()
                .Update(1, Arg.Is<StockTriggerLevelsResource>(r => r.Id == this.requestResource.Id));
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<StockTriggerLevelsResource>();
            resource.LocationId.Should().Be(1);
        }
    }
}
