namespace Linn.Stores.Facade.Tests.ExchangeRateServiceTests
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenSearching : ContextBase
    {
        private IResult<IEnumerable<ImportBookExchangeRate>> results;

        [SetUp]
        public void SetUp()
        {
            var exchangeRates = new List<ImportBookExchangeRate>
                                    {
                                        new ImportBookExchangeRate()
                                            {
                                                PeriodNumber = 1234,
                                                BaseCurrency = "GBP",
                                                ExchangeRate = 1.24m,
                                                ExchangeCurrency = "EUR"
                                            },
                                        new ImportBookExchangeRate
                                            {
                                                PeriodNumber = 1234,
                                                BaseCurrency = "GBP",
                                                ExchangeRate = 1.31m,
                                                ExchangeCurrency = "USD"
                                            }
                                    }.AsQueryable();

            this.ImportBookService.GetExchangeRates(Arg.Any<string>()).Returns(exchangeRates);

            this.results = this.Sut.GetExchangeRatesForDate("01-Jan-0000");
        }

        [Test]
        public void ShouldSearch()
        {
            this.ImportBookService.Received().GetExchangeRates(Arg.Any<string>());
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.results.Should().BeOfType<SuccessResult<IEnumerable<ImportBookExchangeRate>>>();
            var dataResult = ((SuccessResult<IEnumerable<ImportBookExchangeRate>>)this.results).Data.ToList();
            dataResult.FirstOrDefault(
                    x => x.PeriodNumber == 1234 && x.BaseCurrency == "GBP" && x.ExchangeCurrency == "EUR").Should()
                .NotBeNull();
            dataResult.FirstOrDefault(
                    x => x.PeriodNumber == 1234 && x.BaseCurrency == "GBP" && x.ExchangeCurrency == "USD").Should()
                .NotBeNull();
            dataResult.Count.Should().Be(2);
        }
    }
}
