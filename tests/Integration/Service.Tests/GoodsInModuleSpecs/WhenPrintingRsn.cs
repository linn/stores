namespace Linn.Stores.Service.Tests.GoodsInModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenPrintingRsn : ContextBase
    {
        private ProcessResult successResult;

        [SetUp]
        public void SetUp()
        {
            this.successResult = new ProcessResult(true, "Printing...");

            this.Service.PrintRsn(Arg.Any<int>()).Returns(
                new SuccessResult<ProcessResult>(this.successResult));

            this.Response = this.Browser.Post(
                $"/logistics/goods-in/print-rsn",
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
            this.Service.Received().PrintRsn(Arg.Any<int>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ProcessResultResource>();
            resource.Message.Should().Be("Printing...");
        }
    }
}
