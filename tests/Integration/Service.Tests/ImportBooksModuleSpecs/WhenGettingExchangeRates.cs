namespace Linn.Stores.Service.Tests.ImportBooksModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Resources.ImportBooks;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingExchangeRates : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var exchangeRates = new List<ImportBookExchangeRate>
                                    {
                                        new ImportBookExchangeRate
                                            {
                                                BaseCurrency = "GBP",
                                                ExchangeCurrency = "USD",
                                                ExchangeRate = 1.234m,
                                                PeriodNumber = 1482
                                            },
                                        new ImportBookExchangeRate
                                            {
                                                BaseCurrency = "GBP",
                                                ExchangeCurrency = "EUR",
                                                ExchangeRate = 1.000001m,
                                                PeriodNumber = 1482
                                            },
                                    };

            this.ImportBookExchangeRateService.GetExchangeRatesForDate(Arg.Any<string>()).Returns(
                new SuccessResult<IEnumerable<ImportBookExchangeRate>>(exchangeRates));

            this.Response = this.Browser.Get(
                "/logistics/import-books/exchange-rates",
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
            this.ImportBookExchangeRateService.Received().GetExchangeRatesForDate(Arg.Any<string>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<ImportBookExchangeRateResource>>();
            resource.Count().Should().Be(2);
            resource.FirstOrDefault(
                x => x.BaseCurrency == "GBP" && x.ExchangeCurrency == "USD" && x.ExchangeRate == 1.234m
                     && x.PeriodNumber == 1482).Should().NotBeNull();
            resource.FirstOrDefault(
                x => x.BaseCurrency == "GBP" && x.ExchangeCurrency == "EUR" && x.ExchangeRate == 1.000001m
                     && x.PeriodNumber == 1482).Should().NotBeNull();
        }
    }
}
