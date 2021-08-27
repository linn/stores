namespace Linn.Stores.Service.Tests.GoodsInModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.GoodsIn;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenPrintingLabels : ContextBase
    {
        private ProcessResult successResult;

        [SetUp]
        public void SetUp()
        {
            this.successResult = new ProcessResult(true, "Success!");

            this.Service.PrintGoodsInLabels(Arg.Any<PrintGoodsInLabelsRequestResource>()).Returns(
                new SuccessResult<ProcessResult>(this.successResult));

            this.Response = this.Browser.Post(
                $"/logistics/goods-in/print-labels",
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
            this.Service.Received().PrintGoodsInLabels(Arg.Any<PrintGoodsInLabelsRequestResource>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ProcessResultResource>();
            resource.Message.Should().Be("Success!");
        }
    }
}
