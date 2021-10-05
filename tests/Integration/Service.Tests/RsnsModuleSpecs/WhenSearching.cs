namespace Linn.Stores.Service.Tests.RsnsModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;
    using Common.Facade;
    using Domain.LinnApps;
    using Domain.LinnApps.Parts;
    using FluentAssertions;
    using Nancy;
    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;
    using Resources;

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
                            TariffId = 1234,
                            TariffCode = "845512340"
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
                            TariffId = 1234,
                            TariffCode = "845512340"
                        }
                    }
                }
            };

            RsnFacadeService.Search(Arg.Any<string>())
                .Returns(new SuccessResult<IEnumerable<Rsn>>(rsns));

            Response = Browser.Get(
                "/logistics/rsns",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.Query("searchTerm", "11");
                }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            RsnFacadeService.Received().Search(Arg.Any<string>());
        }

        [Test]
        public void ShouldReturnResources()
        {
            var resources = Response.Body.DeserializeJson<IEnumerable<ParcelResource>>();
            resources.Count().Should().Be(2);
        }
    }
}
