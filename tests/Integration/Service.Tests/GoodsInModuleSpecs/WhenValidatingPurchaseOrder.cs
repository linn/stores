namespace Linn.Stores.Service.Tests.GoodsInModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Resources.GoodsIn;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenValidatingPurchaseOrder : ContextBase
    {
        private ValidatePurchaseOrderResult result;

        [SetUp]
        public void SetUp()
        {
            this.result = new ValidatePurchaseOrderResult
                                     {
                                         Message = "Validated!"
                                     };
            this.Service.ValidatePurchaseOrder(Arg.Any<int>(), Arg.Any<int>()).Returns(
                new SuccessResult<ValidatePurchaseOrderResult>(this.result));

            this.Response = this.Browser.Get(
                $"/logistics/purchase-orders/validate/1",
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
            this.Service.Received().ValidatePurchaseOrder(Arg.Any<int>(), Arg.Any<int>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ValidatePurchaseOrderResultResource>();
            resource.Message.Should().Be("Validated!");
        }
    }
}
