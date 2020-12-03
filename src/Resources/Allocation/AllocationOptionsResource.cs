namespace Linn.Stores.Resources.Allocation
{
    public class AllocationOptionsResource
    {
        public string StockPoolCode { get; set; }

        public string DespatchLocationCode { get; set; }

        public int? AccountId { get; set; }

        public string ArticleNumber { get; set; }

        public string AccountingCompany { get; set; }

        public string CutOffDate { get; set; }

        public string CountryCode { get; set; }

        public bool ExcludeUnsuppliableLines { get; set; }

        public bool ExcludeOnHold { get; set; }

        public bool ExcludeOverCreditLimit { get; set; }

        public bool ExcludeNorthAmerica { get; set; }
    }
}
