namespace Linn.Stores.Domain.LinnApps.Sos
{
    using System;

    public class SosOption
    {
        public int JobId { get; set; }

        public string StockPoolCode { get; set; }

        public int? AccountId { get; set; }

        public string ArticleNumber { get; set; }

        public string DespatchLocationCode { get; set; }

        public string AccountingCompany { get; set; }

        public string CountryCode { get; set; }

        public DateTime? CutOffDate { get; set; }
    }
}
