namespace Linn.Stores.Service.Tests.StockLocatorsModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    using Nancy;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenDeleting : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.StockLocatorFacadeService.Delete(Arg.Any<int>())
                .Returns(new SuccessResult<StockLocator>(null));

            this.Response = this.Browser.Delete(
                "/inventory/stock-locators",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.Query("id", "1");
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
            this.StockLocatorFacadeService.Received().Delete(Arg.Any<int>());
        }

        [Test]
        public void ShouldReturnEmpty()
        {
           this.Response.Body.Should().BeEmpty();
        }
    }
}
