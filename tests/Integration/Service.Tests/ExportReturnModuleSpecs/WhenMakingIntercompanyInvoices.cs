namespace Linn.Stores.Service.Tests.RsnModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.RequestResources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenMakingIntercompanyInvoices : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var requestResource = new MakeIntercompanyInvoicesRequestResource { ReturnId = 123 };

            this.ExportReturnService.MakeIntercompanyInvoices(123)
                .Returns(new SuccessResult<ExportReturn>(new ExportReturn { ReturnId = 123 }));

            this.Response = this.Browser.Post(
                "/inventory/exports/returns/make-intercompany-invoices",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Header("Content-Type", "application/json");
                        with.JsonBody(requestResource);
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
            this.ExportReturnService.Received().MakeIntercompanyInvoices(123);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ExportReturnResource>();
            resource.ReturnId.Should().Be(123);
        }
    }
}