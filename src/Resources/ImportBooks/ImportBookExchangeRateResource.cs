namespace Linn.Stores.Resources.ImportBooks
{
    public class ImportBookExchangeRateResource
    {
        public int PeriodNumber { get; set; }

        public string ExchangeCurrency { get; set; }

        public string BaseCurrency { get; set; }

        public decimal? ExchangeRate { get; set; }
    }
}
