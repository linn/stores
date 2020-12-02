namespace Linn.Stores.Resources.Allocation
{
    using Linn.Common.Resources;

    public class SosAllocDetailResource : HypermediaResource
    {
        public int Id { get; set; }

        public int JobId { get; set; }

        public int AccountId { get; set; }

        public int OutletNumber { get; set; }

        public int OrderNumber { get; set; }

        public int OrderLine { get; set; }

        public int QuantitySuppliable { get; set; }

        public string DatePossible { get; set; }

        public string SupplyInFullDate { get; set; }

        public decimal QuantityToAllocate { get; set; }

        public decimal QuantityAllocated { get; set; }

        public decimal UnitPriceIncludingVAT { get; set; }

        public string SupplyInFullCode { get; set; }

        public string OrderLineHoldStatus { get; set; }

        public string ArticleNumber { get; set; }

        public int MaximumQuantityToAllocate { get; set; }

        public string AllocationSuccessful { get; set; }

        public string AllocationMessage { get; set; }
    }
}
