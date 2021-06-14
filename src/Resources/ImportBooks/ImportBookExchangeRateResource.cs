namespace Linn.Stores.Resources.ImportBooks
{
    using Linn.Common.Resources;

    public class ImportBookExchangeRateResource : HypermediaResource
    {
        public int PeriodNumber { get; set; }

        public string ExchangeCurrency { get; set; }

        public string BaseCurrency { get; set; }

        public decimal? ExchangeRate { get; set; }
    }
}
