namespace Linn.Stores.Service.Tests.StockLocatorsModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdating : ContextBase
    {
        private StockLocatorResource requestResource;

        [SetUp]
        public void SetUp()
        {
            this.requestResource = new StockLocatorResource
                                       {
                                           PartNumber = "PART",
                                           Id = 1,
                                           Remarks = "Desc",
                                       };
            var stockLocator = new StockLocator
                                   {
                                       PartNumber = "PART", Id = 1, Remarks = "Desc",
                                   };
            this.StockLocatorFacadeService.Update(1, Arg.Any<StockLocatorResource>())
                .Returns(new SuccessResult<StockLocator>(stockLocator));

            this.Response = this.Browser.Put(
                "inventory/stock-locators/1",
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
            this.StockLocatorFacadeService
                .Received()
                .Update(1, Arg.Is<StockLocatorResource>(r => r.Id == this.requestResource.Id));
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<StockLocatorResource>();
            resource.Id.Should().Be(1);
        }
    }
}
