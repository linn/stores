namespace Linn.Stores.Service.Tests.GoodsInModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.RequestResources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenDoingBookIn : ContextBase
    {
        private ProcessResult successResult;

        [SetUp]
        public void SetUp()
        {
            this.successResult = new ProcessResult
                                     {
                                         Success = true,
                                         Message = "Success!"
            };
            this.Service.DoBookIn(Arg.Any<BookInRequestResource>()).Returns(
                new SuccessResult<ProcessResult>(this.successResult));

            this.Response = this.Browser.Post(
                $"/logistics/book-in",
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
            this.Service.Received().DoBookIn(Arg.Any<BookInRequestResource>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ProcessResultResource>();
            resource.Message.Should().Be("Success!");
        }
    }
}
