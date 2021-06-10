namespace Linn.Stores.Service.Tests.ImportBooksModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Resources.ImportBooks;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingTransportCodes : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var transportCodes = new List<ImportBookTransportCode>
                                     {
                                         new ImportBookTransportCode { TransportId = 1, Description = "Road" },
                                         new ImportBookTransportCode { TransportId = 2, Description = "Rail" },
                                     };

            this.ImportBookTransportCodeFacadeService.GetAll().Returns(
                new SuccessResult<IEnumerable<ImportBookTransportCode>>(transportCodes));

            this.Response = this.Browser.Get(
                "/logistics/import-books/transport-codes",
                with => { with.Header("Accept", "application/json"); }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.ImportBookTransportCodeFacadeService.Received().GetAll();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<ImportBookTransportCodeResource>>();
            resource.Count().Should().Be(2);
            resource.FirstOrDefault(x => x.TransportId == 1 && x.Description == "Road").Should().NotBeNull();
            resource.FirstOrDefault(x => x.TransportId == 2 && x.Description == "Rail").Should().NotBeNull();

        }
    }
}
