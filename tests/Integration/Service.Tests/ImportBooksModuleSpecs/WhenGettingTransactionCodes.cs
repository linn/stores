namespace Linn.Stores.Service.Tests.ImportBooksModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Resources.Parts;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingTransactionCodes : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var transportCodes = new List<ImportBookTransactionCode>
                                     {
                                         new ImportBookTransactionCode { TransactionId = 1, Description = "20" },
                                         new ImportBookTransactionCode { TransactionId = 2, Description = "40" },
                                     };

            this.ImportBookTransactionCodeFacadeService.GetAll().Returns(
                new SuccessResult<IEnumerable<ImportBookTransactionCode>>(transportCodes));

            this.Response = this.Browser.Get(
                "/logistics/import-books/transaction-codes",
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
            this.ImportBookTransactionCodeFacadeService.Received().GetAll();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<ImportBookTransactionCodeResource>>();
            resource.Count().Should().Be(2);
            resource.FirstOrDefault(x => x.TransactionId == 1 && x.Description == "20").Should().NotBeNull();
            resource.FirstOrDefault(x => x.TransactionId == 2 && x.Description == "40").Should().NotBeNull();
        }
    }
}
