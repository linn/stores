namespace Linn.Stores.Service.Tests.InterCompanyInvoiceModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.InterCompanyInvoices;
    using Linn.Stores.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenSearchingByDocumentNumber : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var inv1 = new InterCompanyInvoice {DocumentNumber = 123 };

            this.InterCompanyInvoiceService.GetByDocumentNumber(123).Returns(
            new SuccessResult<InterCompanyInvoice>(inv1));

            this.Response = this.Browser.Get(
                "/inventory/exports/inter-company-invoices/123",
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
            this.InterCompanyInvoiceService.Received().GetByDocumentNumber(123);
        }
       
        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IntercompanyInvoiceResource>();
            resource.DocumentNumber.Should().Be(123);
        }
    }
}