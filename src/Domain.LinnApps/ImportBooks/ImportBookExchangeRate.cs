namespace Linn.Stores.Domain.LinnApps.ImportBooks
{
    public class ImportBookExchangeRate
    {
        public int PeriodNumber { get; set; }

        public string ExchangeCurrency { get; set; }

        public string BaseCurrency { get; set; }

        public decimal? ExchangeRate { get; set; }
    }
}
