namespace Linn.Stores.Domain.LinnApps.Tests.ImportBookServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.ImportBooks;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingExchangeRates : ContextBase
    {
        private IEnumerable<ImportBookExchangeRate> result;

        [SetUp]
        public void SetUp()
        {
            var exchangeRates = new List<ImportBookExchangeRate>
                                    {
                                        new ImportBookExchangeRate
                                            {
                                                BaseCurrency = "GBP",
                                                ExchangeRate = 1.1m,
                                                ExchangeCurrency = "EUR",
                                                PeriodNumber = 1234
                                            },
                                        new ImportBookExchangeRate
                                            {
                                                BaseCurrency = "GBP",
                                                ExchangeRate = 1.3m,
                                                ExchangeCurrency = "DKN",
                                                PeriodNumber = 1234
                                            }
                                    };

            this.LedgerPeriodRepository.FindBy(Arg.Any<Expression<Func<LedgerPeriod, bool>>>())
                .Returns(new LedgerPeriod() { MonthName = "APR2021", PeriodNumber = 1234 });

            this.ExchangeRateRepository.FilterBy(Arg.Any<Expression<Func<ImportBookExchangeRate, bool>>>())
                .Returns(exchangeRates.AsQueryable());

            this.result = this.Sut.GetExchangeRates("24-APR-2021");
        }

        [Test]
        public void ShouldReturnInvoiceDetails()
        {
            this.result.Count().Should().Be(2);
            this.result.FirstOrDefault(
                x => x.BaseCurrency == "GBP" && x.ExchangeRate == 1.3m && x.ExchangeCurrency == "DKN"
                     && x.PeriodNumber == 1234).Should().NotBeNull();
            this.result.FirstOrDefault(
                x => x.BaseCurrency == "GBP" && x.ExchangeRate == 1.1m && x.ExchangeCurrency == "EUR"
                     && x.PeriodNumber == 1234).Should().NotBeNull();
        }

        [Test]
        public void ShouldCallLedgerRepository()
        {
            this.LedgerPeriodRepository.Received().FindBy(Arg.Any<Expression<Func<LedgerPeriod, bool>>>());
        }

        [Test]
        public void ShouldCallExchangeRepository()
        {
            this.ExchangeRateRepository.Received().FilterBy(Arg.Any<Expression<Func<ImportBookExchangeRate, bool>>>());
        }
    }
}
