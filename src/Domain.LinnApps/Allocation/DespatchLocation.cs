namespace Linn.Stores.Domain.LinnApps.Allocation
{
    using System;

    public class DespatchLocation
    {
        public string LocationCode { get; set; }

        public int LocationId { get; set; }

        public DateTime? DateInvalid { get; set; }

        public int Sequence { get; set; }

        public int UnAllocLocationId { get; set; }

        public string DefaultCarrier { get; set; }

        public int Id { get; set; }
    }
}
