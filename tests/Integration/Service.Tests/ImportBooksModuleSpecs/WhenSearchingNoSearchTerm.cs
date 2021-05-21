namespace Linn.Stores.Service.Tests.ImportBooksModuleSpecs
{
    using FluentAssertions;
    using Nancy;
    using NUnit.Framework;

    public class WhenSearchingNoSearchTerm : ContextBase
    {
        [SetUp]
        public void SetUp()
        {

            this.Response = this.Browser.Get(
                "/logistics/import-books",
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
