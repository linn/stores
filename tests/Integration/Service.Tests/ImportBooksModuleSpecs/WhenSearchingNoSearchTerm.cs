namespace Linn.Stores.Service.Tests.ImportBooksModuleSpecs
{
    using System.Collections.Generic;

    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Resources.Parts;
    using Nancy;
    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenSearchingNoSearchTerm : ContextBase
    {
        [SetUp]
        public void SetUp()
        {

            this.Response = this.Browser.Get(
                "/inventory/import-books",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("searchTerm", string.Empty);
                    }).Result;
        }


        [Test]
        public void ShouldReturnBadRequest()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
