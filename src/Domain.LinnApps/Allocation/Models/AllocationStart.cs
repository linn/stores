namespace Linn.Stores.Domain.LinnApps.Allocation.Models
{
    public class AllocationStart
    {
        public AllocationStart(int id)
        {
            this.Id = id;
        }

        public int Id { get; set; }

        public string AllocationNotes { get; set; }

        public string SosNotes { get; set; }
    }
}