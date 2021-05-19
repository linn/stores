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

    public class WhenSearching : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var importBooks = new List<ImportBook> { new ImportBook { Id = 118 }, new ImportBook { Id = 2118 } };

            this.importBooksFacadeService.Search(Arg.Any<string>())
                .Returns(new SuccessResult<IEnumerable<ImportBook>>(importBooks));
            this.Response = this.Browser.Get(
                "/inventory/import-books",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("searchTerm", "118");
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
            this.importBooksFacadeService.Received().Search(Arg.Any<string>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<ImportBookResource>>();
            resource.Count().Should().Be(2);
            resource.FirstOrDefault(x => x.Id == 118).Should().NotBeNull();
            resource.FirstOrDefault(x => x.Id == 2118).Should().NotBeNull();

        }
    }
}