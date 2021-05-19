﻿namespace Linn.Stores.Service.Tests.ImportBooksModuleSpecs
{
    using System;

    using FluentAssertions;
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Resources.Parts;
    using Nancy;
    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenUpdating : ContextBase
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
                                         Weight = (decimal)00.70,
                                         Comments = "Rsn 1234 raised as BRG, but Customs charged duty incorrectly."
                                     };

            this.importBooksFacadeService.Update(Arg.Any<int>(), Arg.Any<ImportBookResource>())
                .Returns(new SuccessResult<ImportBook>(importBook));
            
            this.Response = this.Browser.Put(
                "/inventory/import-books/123",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.JsonBody(importBookResource);
                    }).Result;
        }

        [Test]
        public void ShouldCallService()
        {
            this.importBooksFacadeService.Received().Update(Arg.Any<int>(), Arg.Any<ImportBookResource>());
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ImportBookResource>();
            resource.Id.Should().Be(123);
        }
    }
}