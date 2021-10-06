namespace Linn.Stores.Service.Tests.RsnsModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenSearching : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var rsns = new List<Rsn>
                           {
                               new Rsn
                                   {
                                       RsnNumber = 11818218,
                                       ArticleNumber = "vingt deux",
                                       Quantity = 11,
                                       SalesArticle = new SalesArticle
                                                          {
                                                              ArticleNumber = "vingt deux",
                                                              Description = "the quick broon fox",
                                                              TariffId = 1234,
                                                              Tariff = new Tariff
                                                                           {
                                                                               TariffId = 1234, TariffCode = "845512340"
                                                                           }
                                                          }
                                   },
                               new Rsn
                                   {
                                       RsnNumber = 111111,
                                       ArticleNumber = "trente deux",
                                       Quantity = 22,
                                       SalesArticle = new SalesArticle
                                                          {
                                                              ArticleNumber = "trente deux",
                                                              Description = "the quick yellow fox",
                                                              TariffId = 1234,
                                                              Tariff = new Tariff
                                                                           {
                                                                               TariffId = 1234, TariffCode = "845512340"
                                                                           }
                                                          }
                                   }
                           };

            this.RsnFacadeService.Search(Arg.Any<string>()).Returns(new SuccessResult<IEnumerable<Rsn>>(rsns));

            this.Response = this.Browser.Get(
                "/logistics/rsns",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("searchTerm", "11");
                    }).Result;
        }

        [Test]
        public void ShouldCallService()
        {
            this.RsnFacadeService.Received().Search(Arg.Any<string>());
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldReturnResources()
        {
            var resources = this.Response.Body.DeserializeJson<IEnumerable<ParcelResource>>();
            resources.Count().Should().Be(2);
        }
    }
}
