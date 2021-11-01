namespace Linn.Stores.Service.Tests.GoodsInModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.GoodsIn;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenValidatingRsn : ContextBase
    {
        private ValidateRsnResult result;

        [SetUp]
        public void SetUp()
        {
            this.result = new ValidateRsnResult()
                              {
                                  Message = "Validated!"
                              };
            this.Service.ValidateRsn(1).Returns(
                new SuccessResult<ValidateRsnResult>(this.result));

            this.Response = this.Browser.Get(
                $"/logistics/rsn/validate/1",
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
            this.Service.Received().ValidateRsn(1);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ValidateRsnResult>();
            resource.Message.Should().Be("Validated!");
        }
    }
}
