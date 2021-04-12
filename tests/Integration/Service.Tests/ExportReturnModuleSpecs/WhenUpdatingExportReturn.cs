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

    public class WhenUpdatingExportReturn : ContextBase
    {
        private ExportReturnResource requestResource;

        [SetUp]
        public void SetUp()
        {
            this.requestResource = new ExportReturnResource { ReturnId = 123 };

            this.ExportReturnService.UpdateExportReturn(123, Arg.Is<ExportReturnResource>(e => e.ReturnId == 123))
                .Returns(new SuccessResult<ExportReturn>(new ExportReturn { ReturnId = 123 }));

            this.Response = this.Browser.Put(
                "/inventory/exports/returns/123",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Header("Content-Type", "application/json");
                        with.JsonBody(this.requestResource);
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
            this.ExportReturnService.Received().UpdateExportReturn(
                123,
                Arg.Is<ExportReturnResource>(e => e.ReturnId == 123));
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ExportReturnResource>();
            resource.ReturnId.Should().Be(123);
        }
    }
}