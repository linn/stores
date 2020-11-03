namespace Linn.Stores.Resources
{
    using Linn.Common.Resources;

    public class StockPoolResource : HypermediaResource
    {
        public int Id { get; set; }

        public string StockPoolCode { get; set; }

        public string Description { get; set; }

        public string DateInvalid { get; set; }

        public string AccountingCompany { get; set; }

        public int? Sequence { get; set; }

        public string StockCategory { get; set; }

        public int? DefaultLocation { get; set; }
    }
}
