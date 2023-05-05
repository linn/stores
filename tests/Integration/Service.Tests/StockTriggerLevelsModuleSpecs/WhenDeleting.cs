namespace Linn.Stores.Service.Tests.StockTriggerLevelsModuleSpecs
{
    using System.Collections.Generic;
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenDeleting : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.AuthorisationService.HasPermissionFor(AuthorisedAction.CreateStockTriggerLevel, Arg.Any<IEnumerable<string>>()).Returns(true);
            this.StockTriggerLevelsFacadeService.DeleteStockTriggerLevel(Arg.Any<int>(), 123)
                .Returns(new SuccessResult<StockTriggerLevel>(new StockTriggerLevel()
                {
                    LocationId = 1
                })); 
            this.Response = this.Browser.Delete(
                "/inventory/stock-trigger-levels/1",
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
            this.StockTriggerLevelsFacadeService.Received().DeleteStockTriggerLevel(Arg.Any<int>(), 123);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resultResource = this.Response.Body.DeserializeJson<StockTriggerLevelsResource>();
            resultResource.LocationId.Should().Be(1);
        }
    }
}
