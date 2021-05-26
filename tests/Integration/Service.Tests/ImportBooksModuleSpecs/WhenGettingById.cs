namespace Linn.Stores.Service.Tests.ImportBooksModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Resources.Parts;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingById : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var importBook = new ImportBook { Id = 123 };
            this.ImportBooksFacadeService.GetById(123).Returns(new SuccessResult<ImportBook>(importBook));

            this.Response = this.Browser.Get(
                "/inventory/import-books/123",
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
            this.ImportBooksFacadeService.Received().GetById(123);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<ImportBookResource>();
            resource.Id.Should().Be(123);
        }
    }
}