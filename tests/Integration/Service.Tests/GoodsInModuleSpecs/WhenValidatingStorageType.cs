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

    public class WhenValidatingStorageType : ContextBase
    {
        private ValidateStorageTypeResult result;

        [SetUp]
        public void SetUp()
        {
            this.result = new ValidateStorageTypeResult
                            {
                                  Message = "Validated!"
                            };
            this.Service.ValidateStorageType(Arg.Any<ValidateStorageTypeRequestResource>()).Returns(
                new SuccessResult<ValidateStorageTypeResult>(this.result));

            this.Response = this.Browser.Get(
                $"/logistics/goods-in/validate-storage-type",
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
            this.Service.Received().ValidateStorageType(Arg.Any<ValidateStorageTypeRequestResource>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ValidateStorageTypeResultResource>();
            resource.Message.Should().Be("Validated!");
        }
    }
}
