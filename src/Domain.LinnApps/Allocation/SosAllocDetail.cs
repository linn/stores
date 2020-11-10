namespace Linn.Stores.Domain.LinnApps.Allocation
{
    using System;

    public class SosAllocDetail
    {
        public int Id { get; set; }

        public int JobId { get; set; }

        public int AccountId { get; set; }

        public int OutletNumber { get; set; }

        public int OrderNumber { get; set; }

        public int OrderLine { get; set; }

        public int QuantitySuppliable { get; set; }

        public DateTime? DatePossible { get; set; }

        public DateTime? SupplyInFullDate { get; set; }

        public decimal QuantityToAllocate { get; set; }

        public decimal QuantityAllocated { get; set; }

        public decimal UnitPriceIncludingVAT { get; set; }

        public string SupplyInFullCode { get; set; }

        public string OrderLineHoldStatus { get; set; }

        public string ArticleNumber { get; set; }
    }
}
