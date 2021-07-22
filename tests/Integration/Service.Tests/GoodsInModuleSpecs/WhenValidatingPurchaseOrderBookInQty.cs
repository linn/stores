namespace Linn.Stores.Service.Tests.GoodsInModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Resources.GoodsIn;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenValidatingPurchaseOrderBookInQty : ContextBase
    {
        private ProcessResult result;

        [SetUp]
        public void SetUp()
        {
            this.result = new ProcessResult
                              {
                                  Success = false,
                                  Message = "Order is full Booked!"
                              };
            this.Service.ValidatePurchaseOrderQty(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>()).Returns(
                new SuccessResult<ProcessResult>(this.result));

            this.Response = this.Browser.Get(
                $"/logistics/purchase-orders/validate-qty",
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
            this.Service.Received().ValidatePurchaseOrderQty(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ProcessResult>();
            resource.Message.Should().Be("Order is full Booked!");
        }
    }
}
