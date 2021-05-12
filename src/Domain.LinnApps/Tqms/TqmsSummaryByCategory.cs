namespace Linn.Stores.Domain.LinnApps.Tqms
{
    public class TqmsSummaryByCategory
    {
        public string JobRef { get; set; }

        public string HeadingCode { get; set; }

        public string HeadingDescription { get; set; }

        public string CategoryCode { get; set; }

        public string CategoryDescription { get; set; }

        public decimal TotalValue { get; set; }

        public int? HeadingOrder { get; set; }

        public int? CategoryOrder { get; set; }

        public string ActiveCategory { get; set; }
    }
}
