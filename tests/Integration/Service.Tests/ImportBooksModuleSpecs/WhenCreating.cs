namespace Linn.Stores.Service.Tests.ImportBooksModuleSpecs
{
    using System;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Resources.ImportBooks;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenCreating : ContextBase
    {
        private readonly DateTime now = DateTime.Now;

        [SetUp]
        public void SetUp()
        {
            var importBookResource = new ImportBookResource
                                     {
                                         ParcelNumber = 4,
                                         SupplierId = 2,
                                         DateCreated = this.now.ToString("o"),
                                         CarrierId = 4,
                                         Weight = 00.70m,
                                         Comments = "Rsn 1234 raised as BRG, but Customs charged duty incorrectly."
                                     };

            var importBook = new ImportBook { Id = 12345 };

            this.ImportBooksFacadeService.Add(Arg.Any<ImportBookResource>())
                .Returns(new CreatedResult<ImportBook>(importBook));

            this.Response = this.Browser.Post(
                "/logistics/import-books",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.JsonBody(importBookResource);
                }).Result;
        }

        [Test]
        public void ShouldCallService()
        {
            this.ImportBooksFacadeService.Received().Add(Arg.Any<ImportBookResource>());
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ImportBookResource>();
            resource.Id.Should().Be(12345);
        }
    }
}
