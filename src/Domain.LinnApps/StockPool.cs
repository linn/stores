namespace Linn.Stores.Domain.LinnApps
{
    using System;

    public class StockPool
    {
        public int Id { get; set; }

        public string StockPoolCode { get; set; }

        public string Description { get; set; }

        public DateTime? DateInvalid { get; set; }

        public string AccountingCompany { get; set; }

        public int Sequence { get; set; }

        public string StockCategory { get; set; }

        public int DefaultLocation { get; set; }
    }
}
