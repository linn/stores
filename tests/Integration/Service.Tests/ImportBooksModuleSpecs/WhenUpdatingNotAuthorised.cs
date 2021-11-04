namespace Linn.Stores.Service.Tests.ImportBooksModuleSpecs
{
    using System;
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Resources.ImportBooks;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdatingNotAuthorised : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var importBook = new ImportBook { Id = 123 };

            var importBookResource = new ImportBookResource
                                         {
                                             Id = 123,
                                             ParcelNumber = 4,
                                             SupplierId = 2,
                                             DateCreated = new DateTime().ToString("o"),
                                             CarrierId = 4,
                                             Weight = 00.70m,
                                             Comments = "Rsn 1234 raised as BRG, but Customs charged duty incorrectly.",
                                             Pva = "Y"
                                         };

            this.AuthorisationService.HasPermissionFor("import-books.admin", Arg.Any<IEnumerable<string>>())
                .Returns(false);

            this.Response = this.Browser.Put(
                "/logistics/import-books/123",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.JsonBody(importBookResource);
                    }).Result;
        }

        [Test]
        public void ShouldReturnBadRequest()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
