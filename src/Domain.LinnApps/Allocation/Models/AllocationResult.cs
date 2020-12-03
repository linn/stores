namespace Linn.Stores.Domain.LinnApps.Allocation.Models
{
    public class AllocationResult
    {
        public AllocationResult()
        {
        }

        public AllocationResult(int id)
        {
            this.Id = id;
        }

        public int Id { get; set; }

        public string AllocationNotes { get; set; }

        public string SosNotes { get; set; }
    }
}
