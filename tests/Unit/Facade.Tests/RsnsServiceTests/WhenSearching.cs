namespace Linn.Stores.Facade.Tests.RsnsServiceTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Parts;

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
                                       RsnNumber = 1118218,
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
            this.RsnRepository.FilterBy(Arg.Any<Expression<Func<Rsn, bool>>>()).Returns(rsns.AsQueryable());
        }

        [Test]
        public void ShouldCallSearch()
        {
            this.Sut.Search("111");
            this.RsnRepository.Received().FilterBy(Arg.Any<Expression<Func<Rsn, bool>>>());
        }

        [Test]
        public void ShouldReturnOnlyValidByDefault()
        {
            var result = this.Sut.Search("111");
            result.Should().BeOfType<SuccessResult<IEnumerable<Rsn>>>();
            var dataResult = ((SuccessResult<IEnumerable<Rsn>>) result).Data;
            dataResult.FirstOrDefault(x => x.RsnNumber == 1118218 && x.ArticleNumber == "vingt deux").Should()
                .NotBeNull();
            dataResult.FirstOrDefault(x => x.RsnNumber == 111111 && x.ArticleNumber == "trente deux").Should()
                .NotBeNull();
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            var result = this.Sut.Search("111");
            result.Should().BeOfType<SuccessResult<IEnumerable<Rsn>>>();
        }
    }
}
