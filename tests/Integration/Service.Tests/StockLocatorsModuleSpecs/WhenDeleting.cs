namespace Linn.Stores.Service.Tests.StockLocatorsModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Resources.StockLocators;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenDeleting : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.StockLocatorFacadeService.Delete(Arg.Any<StockLocatorResource>())
                .Returns(new SuccessResult<StockLocator>(new StockLocator
                                                             {
                                                                 Id = 1
                                                             }));

            this.Response = this.Browser.Delete(
                "/inventory/stock-locators/1",
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
            this.StockLocatorFacadeService.Received().Delete(Arg.Any<StockLocatorResource>());
        }

        [Test]
        public void ShouldReturnDeleted()
        {
            var resource = this.Response.Body.DeserializeJson<StockLocatorResource>();
            resource.Id.Should().Be(1);
        }
    }
}
