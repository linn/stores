namespace Linn.Stores.Service.Tests.RsnModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingExportReturn : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.ExportReturnService.GetById(123)
                .Returns(new SuccessResult<ExportReturn>(new ExportReturn { ReturnId = 123 }));

            this.Response = this.Browser.Get(
                "/inventory/exports/returns/123",
                with =>
                    {
                        with.Header("Accept", "application/json");
                    }).Result;
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.ExportReturnService.Received().GetById(123);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ExportReturnResource>();
            resource.ReturnId.Should().Be(123);
        }
    }
}