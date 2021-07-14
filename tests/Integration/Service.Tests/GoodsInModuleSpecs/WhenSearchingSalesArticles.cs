namespace Linn.Stores.Service.Tests.GoodsInModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenSearchingSalesArticles : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.SalesArticleService.SearchLiveSalesArticles(Arg.Any<string>())
                .Returns(new SuccessResult<IEnumerable<SalesArticle>>(new List<SalesArticle>
                                                                          {
                                                                              new SalesArticle { ArticleNumber = "AA" },
                                                                              new SalesArticle { ArticleNumber = "AB" }
                                                                          }));

            this.Response = this.Browser.Get(
                "/inventory/sales-articles",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("searchTerm", "A");
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
            this.SalesArticleService.Received().SearchLiveSalesArticles(Arg.Any<string>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<SalesArticleResource>>().ToList();
            resource.Should().HaveCount(2);
            resource.Should().Contain(a => a.ArticleNumber == "AA");
            resource.Should().Contain(a => a.ArticleNumber == "AB");
        }
    }
}
