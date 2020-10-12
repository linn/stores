namespace Linn.Stores.Resources.Allocation
{
    using Linn.Common.Resources;

    public class DespatchLocationResource : HypermediaResource
    {
        public int Id { get; set; }

        public string LocationCode { get; set; }

        public int? LocationId { get; set; }

        public string DateInvalid { get; set; }

        public int? Sequence { get; set; }

        public int? UnAllocLocationId { get; set; }

        public string DefaultCarrier { get; set; }
    }
}