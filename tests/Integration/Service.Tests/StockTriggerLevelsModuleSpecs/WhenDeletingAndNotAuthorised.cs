namespace Linn.Stores.Service.Tests.StockTriggerLevelsModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    using Nancy;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenDeletingAndNotAuthorised : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
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
        public void ShouldReturnBadRequest()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
