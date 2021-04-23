namespace Linn.Stores.Service.Tests.InterCompanyInvoiceModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenSearchingInterCompanyInvoices : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var inv1 = new InterCompanyInvoice { ExportReturnId = 123, DocumentNumber = 321 };
            var inv2 = new InterCompanyInvoice { ExportReturnId = 123, DocumentNumber = 111 };

            this.InterCompanyInvoiceService.SearchInterCompanyInvoices("123").Returns(
                new SuccessResult<IEnumerable<InterCompanyInvoice>>(new List<InterCompanyInvoice> { inv1, inv2 }));

            this.Response = this.Browser.Get(
                "/inventory/exports/inter-company-invoices",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("searchTerm", "123");
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
            this.InterCompanyInvoiceService.Received().SearchInterCompanyInvoices("123");
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<IntercompanyInvoiceResource>>().ToList();
            resource.Should().HaveCount(2);
        }
    }
}