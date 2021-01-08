namespace Linn.Stores.Resources.Allocation
{
    using Linn.Common.Resources;

    public class AllocationResource : HypermediaResource
    {
        public int Id { get; set; }

        public string AllocationNotes { get; set; }

        public string SosNotes { get; set; }
    }
}
